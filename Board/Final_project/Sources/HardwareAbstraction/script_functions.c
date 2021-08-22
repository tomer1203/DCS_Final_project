/*
 * script_functions.c
 *
 *  Created on: Aug 21, 2021
 *      Author: tomer
 */
#include "TFC.h"
int delay = 50;
// 01
void WaitDelay(int d){
	// set pit delay to d
	SetPITxInterval(0,d);
	// start pit 
	enablePITx(0,TRUE);
	while (!PitDelayDone){ 
		wait(); // wait for pit interrupt
	}
	// shut down pit
	enablePITx(0,FALSE);
	PitDelayDone = FALSE;
}
void blink_rgb(int times){
	int i = 0,j = 0;
	for (i=0;i<times; i++){
		
		for (j=0;j<8;j++){
			RGB_LED_OFF;
			if (j&1){
				RED_LED_ON;
			}
			if (j&2){
				GREEN_LED_ON;
			}
			if (j&4){
				BLUE_LED_ON;
			}
			WaitDelay(delay);
		}
	}
}
// 02
void lcd_count_up(int times){
	int i = 0,j=times;
	char str[3];
	for (i=0;i<times; i++){
		for (j=0; j<=10;j++){
			sprintf(str,"%d",j);
			Print(str);
			WaitDelay(delay);
		}
	}	
}
// 03
void lcd_count_down(int times){
	int i = 0,j=10;
	char str[3];
	for (i=0;i<times; i++){
		for (j=10; j>=0;j--){
			sprintf(str,"%d",j);
			Print(str);
			WaitDelay(delay);
		}
	}
}
// 04
void set_delay(int new_delay){
	delay = 10 * new_delay;
}
// 05
void clear_all_leds(){
	RGB_LED_OFF;
}
// 06
void servo_deg(int degree){
	WriteServo(degree);
	// TODO: Send degree and distance to pc
}
// 07
void servo_scan(int left_angle,int right_angle){
	// TODO: 
}
// 08
void sleep(){
	wait(); // TODO: maybe turn this to stop?
}
