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
	enterON = FALSE;
	scroll_downON = FALSE;
	print_uiON = FALSE;

	while(1){
		if (enterON){
			StateModes next_state = menu.enter_callback();
			state = next_state;
			enterON = FALSE;
		}
		if (scroll_downON){
			menu.scroll_down_callback();
			scroll_downON = FALSE;
		}
		if (print_uiON){
			menu.print_callback();
			print_uiON = FALSE;
		}
		wait();
//		switch (state){
//		
//		case IDLE_E:
//			wait(); 
//			break;
//		
//		}
//		case SCRIPT_E:
//			break;
//		case TELEMETER_E:
//			break;
	}
	
}

