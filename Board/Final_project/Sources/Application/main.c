/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable
//
//distance_ready = 0;
//rising_edge=0;
//falling_edge=0;		  
//signal_taken=0;	  
int distance_ready = 0;
unsigned int distance;
volatile float rising_edge = 0;
volatile float falling_edge = 0;
volatile unsigned int signal_taken = 0;	  

int main(void){
	InitGPIO();
	InitTimers();
	InitUARTs();
	
	InitTPMx(0);
	InitTPMx(1);
	InitTPMx(2);
	StartTPMx(1, TRUE);
	
	InitPIT();
	EnablePITModule(TRUE);

	
    InitServo();
    InitSensors();
    enable_sensor(FALSE);
    init_hal();
	initialize_ui();
	initialize_file_system();
	print_ui();
	
	Fsm();
	
	return 0;
	
}



