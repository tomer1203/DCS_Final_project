/*
 * Servo.h
 *
 *  Created on: Aug 19, 2021
 *      Author: им
 */

#ifndef SERVO_H_
#define SERVO_H_


#define PIT_RISING 0
#define PIT_FALLING 1
#define SERVO_TPM 0
#define SENSOR_TRIG 1
#define SENSOR_ECHO 2
#define SERVO_DEG_MAX 180
#define SERVO_DEG_MIN 0
#define TPM_DC_VAL_MIN 900//3276 //  5% * 20ms = 1ms 
#define TPM_DC_VAL_MAX 3750//1875//6554 // 10% * 20ms = 2ms

void InitServo();
void InitSensors();
void WriteServo(int deg);
void SweepServo();


#endif /* SERVO_H_ */
