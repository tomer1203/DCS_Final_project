/*
 * halGPIO.c
 *
 *  Created on: May 5, 2021
 *      Author: ��
 */

#include "TFC.h"
void ftoa(float n, char* res, int afterpoint);
float arr[100];
int arr_index = 0;

//-----------------------------------------------------------------
//  PORTD - ISR = Interrupt Service Routine
//-----------------------------------------------------------------
void PORTD_IRQHandler(void){
	volatile unsigned int i;
	// check that the interrupt was for switch
	if (PORTD_ISFR & PTD_7) {
		if(!(GPIOD_PDIR & PTD_7)){
			RED_LED_TOGGLE;
			scroll_down();
		}
	}
	if(PORTD_ISFR & PTD_6){
		if(!(GPIOD_PDIR & PTD_6)){
			enter();
			BLUE_LED_TOGGLE;
		}
	}
	//Debounce or using PFE field
	while(!(GPIOD_PDIR & PTD_7) );// wait of release the button
	while(!(GPIOD_PDIR & PTD_6) );// wait of release the button
	for(i=10000 ; i>0 ; i--); //delay, button debounce
	
	print_ui();
	DelayMs(100);
	
	PORTD_ISFR |= PTD_7;  // clear interrupt flag bit of PTD7
	PORTD_ISFR |= PTD_6;  // clear interrupt flag bit of PTD6
}

//-----------------------------------------------------------------
// PIT - ISR = Interrupt Service Routine
//-----------------------------------------------------------------
void PIT_IRQHandler(){
	
    if(PIT_TFLG0 & PIT_TFLG_TIF_MASK){
        enablePITx(PIT_FALLING,1);
        GPIOC_PSOR |= PORT_LOC(7);
        StartTPMx(0, TRUE);
        PIT_TFLG0 = PIT_TFLG_TIF_MASK; //clear the Pit 0 Irq flag 
        //RED_LED_ON;
        
    }
    
    if(PIT_TFLG1 & PIT_TFLG_TIF_MASK){
        enablePITx(PIT_FALLING,0);
        GPIOC_PCOR |= PORT_LOC(7);
        PIT_TFLG1 = PIT_TFLG_TIF_MASK; //clear the Pit 1 Irq flag
        //RED_LED_OFF;
        
    }
	
}

//-----------------------------------------------------------------
//  Ultra-sonic sensor Echo  - ISR
//-----------------------------------------------------------------
void FTM0_IRQHandler(){
	GREEN_LED_TOGGLE;
	if (signal_taken == FALSE){    // Capture Rising Edge 
		rising_edge = TPM0_C2V;    // Time of rising edge in Echo pulse
		signal_taken = TRUE;
	}
	else{						   // Capture Falling Edge 
		falling_edge = TPM0_C2V;   
		signal_taken = FALSE;
		if(falling_edge < rising_edge)
			return;
		distance_ready = TRUE;
		distance = (falling_edge - rising_edge)/43.3;  //Calculate distance from sensor (in room temp)
		arr[arr_index++] = distance;
		if(arr_index == 100) 
			arr_index = 0;
		//StartTPMx(0, FALSE);
		clearTPM0();
	}
	TPM0_C2SC |= TPM_CnSC_CHF_MASK; //Manual flag down of the timer
}


// format:
// "$[Br]9600\0"
// $ - command symbol
// Br -Baud rate configuration

//-----------------------------------------------------------------
//  UART0 - ISR
//-----------------------------------------------------------------
void UART0_IRQHandler(){
	uint8_t Char;
	char length[4];
	
	
	if(UART0_S1 & UART_S1_RDRF_MASK){ // RX buffer is full and ready for reading
		Char = UART0_D;
		
		// insert read char to buffer
		string_buffer[string_index] = Char;
		input_string_length--;

		// read the input string length
		if (string_index == 10){
			length[0] = string_buffer[7];
			length[1] = string_buffer[8];
			length[2] = string_buffer[9];
			length[3] = '\0';
			input_string_length = atoi(length); 
		}
		
		// if message is finished		
		if (input_string_length<=0 && string_index >= 10){
			handleMessage();
			return;
		}
		string_index++;
	}
	
}// END UART_IRQ


//////////////////////////////
//    Handle Message
//////////////////////////////
void handleMessage(){
	
	int function_return_value;
	char text[2];
	
	// CHECKSUM Check //
	if (!validate_checksum(string_buffer, string_index + 1)) {
		send2pc(TYPE.STATUS, STATUS.CHECKSUM_ERROR);
		clear_string_buffer();
		return;
	}
	else {
		//send2pc(TYPE.STATUS, STATUS.OK);
	}
	
	switch (state) {
	 
	case WRITING_FILE_INIT_E:
		function_return_value = write_file_init_message(string_buffer);
		if (function_return_value == 1) {
			Print("Receiving a File");
			state = WRITING_FILE;
		}
		if (function_return_value<0){
			sprintf(text,"%d",function_return_value);
			send2pc(TYPE.FILE_END, abs(function_return_value));
		}
		break;
		
	case WRITING_FILE:
		function_return_value = write_file_chunck(string_buffer, string_index-10);
		if (function_return_value == 1) {
			// File written successfully 
			send2pc(TYPE.FILE_END, STATUS.OK);
			Print_two_lines("Received", "Successfully");
			state = IDLE_E;
			initialize_ui();
		}
		if (function_return_value<0){
			sprintf(text,"%d",function_return_value);
			send2pc(TYPE.FILE_END, abs(function_return_value));
		}
		break;
		
	default:
		// ACTIONS //
		
		// print message to chat
		if (is_chat_command(string_buffer)) {
			Print(strip_command(string_buffer));
		}
		// receiving a file
		else if (is_write_file_transfer_command(string_buffer)) {
			state = WRITING_FILE_INIT_E;
		}
		// change Baud rate
		else if (is_br_command(string_buffer)) {
			changeBaudrate();
		}
		break;
	}
	
	
	// when finished reading message, clean the buffer.
	clear_string_buffer();
	
} // Handle Message


///////////////////////////////////////
// Changes baudrate & sends STATUS OK
///////////////////////////////////////
void changeBaudrate(){
	char baudRate[6];
	
	baud_config = atoi(strip_command(string_buffer));
	send2pc(TYPE.BAUDRATE, strip_command(string_buffer));
	DelayMs(500);
	change_Baud_config(baud_config);
	DelayMs(500);
	send2pc(TYPE.STATUS, STATUS.OK);
	Print_two_lines("Baud Rate:", strip_command(string_buffer));
	sprintf(baudRate, "%5d", baud_config);
	
	baud_menu.title[6] = baudRate[0];
	baud_menu.title[7] = baudRate[1];
	baud_menu.title[8] = baudRate[2];
	baud_menu.title[9] = baudRate[3];
	baud_menu.title[10] = baudRate[4];
}



/*
 * Initialises Timers
 */
void InitTimers(){
	InitPIT();
	ClockSetupTPM();
	InitTPMx(0);
}

/*
 * Print to LCD
 */
void Print(const char * s){
	
	cursor_off;
	lcd_clear();
	lcd_goto(0);
	DelayMs(5);
	
	lcd_puts(s);
	
}
void Print_two_lines(const char *s1,const char *s2){
	lcd_clear();
	lcd_goto(0);
	DelayMs(5);
	lcd_puts(s1);
	lcd_new_line;
	lcd_puts(s2);
}

//******************************************************************
// Delay usec functions
//******************************************************************
void DelayUs(unsigned int cnt){
  
	unsigned int i;
        for(i=(4347 * cnt)/1000 ; i>0 ; i--)
        		asm("nop"); // tha command asm("nop") takes raphly 1usec
        
	
}
//******************************************************************
// Delay msec functions
//******************************************************************
void DelayMs(unsigned int cnt){
  
	unsigned int i;
        for(i=cnt ; i>0 ; i--)
        	DelayUs(1000); // tha command asm("nop") takes raphly 1usec
        
}
//******************************************************************
