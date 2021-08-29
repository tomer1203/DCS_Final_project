/*
 * script_functions.c
 *
 *  Created on: Aug 21, 2021
 *      Author: tomer
 */
#include "TFC.h"
int delay = 500;
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
	char msg[20] = {0};
	int i = 0;
	WriteServo(degree);
	enable_sensor(TRUE);
	for(i = 0 ; i < DIST_AVG_SIZE + 1; i++){
		distance_ready = FALSE;
		while(!distance_ready);
	}
	
	if (distance_ready){
		build_scan_msg(msg,out_distance,degree);
		send2pc("Sc",msg);
		Print("Telemetry");
		distance_ready = FALSE;
	}
	enable_sensor(FALSE);
}
// 07
void servo_scan(int left_angle,int right_angle){
	int angle = left_angle;
	char msg[20] = {0};
	WriteServo(left_angle);
	enable_sensor(TRUE);
	while(angle<right_angle){
		while(!distance_ready){
					WaitDelay(10);
		}
		if (distance_ready){
			build_scan_msg(msg,out_distance,angle);
			send2pc("Sc",msg);
			Print("Scanning");
			distance_ready = FALSE;
		}
		angle+=SERVO_DEG_CHANGE;
		WriteServo(angle);

	}
	enable_sensor(FALSE);

}
// 08
void sleep(){
	wait();
}
