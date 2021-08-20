/*
 * halGPIO.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef HALGPIO_H_
#define HALGPIO_H_
// 1/2 KB
#define MAX_STRING 524
#define PACKET_SIZE 128

char string_buffer[MAX_STRING];
int string_index;
int input_string_length;
int baud_config;
char DataReadyDis;

float distance;
volatile float captureR;		  //Input capture - rising
volatile float captureF;		  //Input capture - falling
volatile unsigned int captureFlag;	  //Input capture - Flag


void DelayUs(unsigned int cnt);
void DelayMs(unsigned int cnt);
void InitTimers();
int translate_config_command(char* string);
void Print_two_lines(const char *s1,const char *s2);
void Print(const char * s);
void changeBaudrate();
void handleMessage();

#endif /* HALGPIO_H_ */
