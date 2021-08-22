/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable
//
//distance_ready = 0;
//rising_edge=0;
//falling_edge=0;		  
//signal_taken=0;	  
char distance_ready = 0;
float distance;
volatile float rising_edge = 0;
volatile float falling_edge = 0;
volatile unsigned int signal_taken = 0;	  

int main(void){

	ClockSetup();
	InitGPIO();
	InitTimers();

	InitUARTs();
	
    InitServo();
    InitSensors();
//	RED_LED_TOGGLE;
//	DelayMs(2000);
//	RED_LED_TOGGLE;
//	
//	BLUE_LED_TOGGLE;
//	WriteServo(180);
//	DelayMs(2000);
//	BLUE_LED_TOGGLE;
//	
//	SweepServo();
	
	//enablePIT(1);
	//disablePITx(1);
	
	initialize_ui();
	initialize_file_system();
	print_ui();
	
	Fsm();
	return 0;
	
}



