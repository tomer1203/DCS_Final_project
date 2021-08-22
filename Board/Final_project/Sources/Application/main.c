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
char distance_ready = 0;
float distance;
volatile float rising_edge = 0;
volatile float falling_edge = 0;
volatile unsigned int signal_taken = 0;	  

int main(void){

	InitGPIO();
	InitTimers();

	InitADCs();
	InitUARTs();
	
	InitServo();
	
	EnablePITModule(TRUE);
    InitServo();
    InitSensors();
	
	initialize_ui();
	initialize_file_system();
	print_ui();
	
	Fsm();
	
	return 0;
	
}



