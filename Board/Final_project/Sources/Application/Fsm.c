/*
 * Fsm.c
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#include "TFC.h"
void change_state(StateModes next_state){
	state = next_state;
}
void menu_control(uint8_t digit){
		
}

void Fsm(void){
	state = IDLE_E;
	lastState = state;
	interval = 0;
	char distance_ascii[20];
	
	while(1){
		
		switch (state){
		
		case IDLE_E:
			//wait(); 
			if (DataReadyDis)
			{
				DataReadyDis = 0;
				ftoa(distance,distance_ascii,2);
				Print(distance_ascii);

//				UARTprintf(UART0_BASE_PTR,disString);
//				UARTprintf(UART0_BASE_PTR,"\r\n");

			}
			break;
			
		}
	}
	
}

