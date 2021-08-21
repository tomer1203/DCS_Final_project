/*
 * script_functions.c
 *
 *  Created on: Aug 21, 2021
 *      Author: tomer
 */
#include "TFC.h"

// 01
void blink_rgb(int times){
	int i = 0;
	for (i=0;i<times; i++){
		//blink
	}
}
// 02
void lcd_count_up(int times){
	int i = 0,j=times;
	char str[3];
	for (i=0;i<times; i++){
		for (j=0; j<10;j++){
			Print(sprintf(str,"%d",j));
		}
	}	
}
// 03
void lcd_count_down(int times){
	int i = 0,j=times;
	char str[3];
	for (i=0;i<times; i++){
		for (j=10; j>0;j--){
			Print(sprintf(str,"%d",j));
		}
	}
}
// 04
void set_delay(int new_delay){
	d = new_delay;
}
// 05
void clear_all_leds(){
	
}
// 06
void servo_deg(int degree){
	
}
// 07
void servo_scan(int left_angle,int right_angle){
	
}
// 08
void sleep(){
	wait(); // TODO: maybe turn this to stop?
}
