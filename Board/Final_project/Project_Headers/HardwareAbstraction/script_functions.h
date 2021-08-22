/*
 * script_functions.h
 *
 *  Created on: Aug 21, 2021
 *      Author: tomer
 */

#ifndef SCRIPT_FUNCTIONS_H_
#define SCRIPT_FUNCTIONS_H_

extern int delay;

void blink_rgb(int times);
void lcd_count_up(int times);
void lcd_count_down(int times);
void set_delay(int new_delay);
void clear_all_leds();
void servo_deg(int degree);
void servo_scan(int left_angle,int right_angle);
void sleep();

#endif /* SCRIPT_FUNCTIONS_H_ */
