#include "TFC.h"


//////////////////////////////////
//	Inits Servo
/////////////////////////////////
void InitServo(){
	ClockSetupTPM(); // initialise tpm clock
	InitTPMx(SERVO_TPM);		 // configure pit 0
	StartTPMx(SERVO_TPM, 1); // start pit 
}
/////////////////////////////////////
//  Inits Sensors
/////////////////////////////////////
void InitSensors(){
	ClockSetupTPM(); // initialise tpm clock
	InitTPMx(SENSOR_TRIG);
	InitTPMx(SENSOR_ECHO);
}
//////////////////////////////////
//	Change Servo's deg
/////////////////////////////////
void WriteServo(int deg){
	int dutyCycle;
	
	// check if deg is valid
	if( SERVO_DEG_MAX < deg || deg < SERVO_DEG_MIN ){
		return;
	}
	
	StartTPMx(SERVO_TPM, 0);
	dutyCycle = TPM_DC_VAL_MIN  + (TPM_DC_VAL_MAX - TPM_DC_VAL_MIN) * (float)deg/(float)SERVO_DEG_MAX ;
	SetTPMxDutyCycle(SERVO_TPM, dutyCycle);
	StartTPMx(SERVO_TPM, 1);
	DelayMs(4*180/9);

}

//////////////////////////////////////
//	Sweep Servo 180 deg back & forth
//////////////////////////////////////
void SweepServo(){
	// TODO: work with PIT
	
	int deg;
	for(deg=0 ; deg < 180 ; deg+=9)
		WriteServo(deg);
}
