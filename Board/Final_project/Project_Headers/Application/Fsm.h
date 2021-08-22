/*
 * Fsm.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef FSM_H_
#define FSM_H_

#include <stdint.h>

#define NL 218


int interval;
int enterON;
int scroll_downON;
int print_uiON;

typedef enum StateModes{
	IDLE_E,
	WRITING_FILE_INIT_E,
	WRITING_FILE,
	RADAR_DETECT_E,
	TELEMETER_E,
	SCRIPT_E,
	CONFIGURATION_E} StateModes;
volatile StateModes state;

StateModes lastState;

void Fsm(void);
void menu_control(uint8_t digit);





#endif /* FSM_H_ */
