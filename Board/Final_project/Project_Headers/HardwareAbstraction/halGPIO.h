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
volatile unsigned int distances[DIST_AVG_SIZE];
volatile int distance_index;
// bools
extern volatile int distance_ready;
extern volatile int signal_taken;	
// distance variables
extern volatile unsigned int acc_distance;
extern volatile unsigned int out_distance;   

extern volatile unsigned int distance;
extern volatile unsigned int rising_edge;
extern volatile unsigned int falling_edge;



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
void ResetDistanceAccumulator();

#endif /* HALGPIO_H_ */
