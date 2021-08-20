#include "TFC.h"

#define SERVO_TPM 0
#define SERVO_DEG_MAX 180
#define SERVO_DEG_MIN 0
#define TPM_DC_VAL_MIN 3277 //  5% * 20ms = 1ms
#define TPM_DC_VAL_MAX 6554 // 10% * 20ms = 2ms


//////////////////////////////////
//	Inits Servo
/////////////////////////////////
void InitServo(){
	ClockSetupTPM(); // initialise tpm clock
	InitTPM(SERVO_TPM);		 // configure pit 0
	startTPMx(SERVO_TPM, 1); // start pit 0
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
	
	dutyCycle = TPM_DC_VAL_MIN *( deg / SERVO_DEG_MAX + 1);
	SetTPMxDutyCycle(SERVO_TPM, dutyCycle);
}

//////////////////////////////////////
//	Sweep Servo 180 deg back & forth
//////////////////////////////////////
void sweepServo(){
	// TODO: work with PIT
	
	int deg;
	for(deg=0 ; deg < 180 ; deg++)
		WriteServo(deg);
}
