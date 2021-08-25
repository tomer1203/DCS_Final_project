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
#define DIST_AVG_SIZE 8
#define LOG2_DIST_AVG_SIZE 3
char string_buffer[MAX_STRING];
int string_index;
int input_string_length;
int baud_config;

int PitDelayDone;

// distance array
unsigned int distances[DIST_AVG_SIZE];
int distance_index;
unsigned int acc_distance;
unsigned int out_distance;

extern int distance_ready;
unsigned int distance;
extern volatile float rising_edge;
extern volatile float falling_edge;
extern volatile unsigned int signal_taken;	  


void DelayUs(unsigned int cnt);
void DelayMs(unsigned int cnt);
void InitTimers();
void init_sensor_hal();
void enable_sensor(int enable);
int translate_config_command(char* string);
void Print_two_lines(const char *s1,const char *s2);
void Print(const char * s);
void changeBaudrate();
void handleMessage();
void init_hal();

#endif /* HALGPIO_H_ */
