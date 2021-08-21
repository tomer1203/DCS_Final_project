/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable

distance_ready = 0;
rising_edge=0;
falling_edge=0;		  
signal_taken=0;	  

int main(void){
	
	ClockSetup();
	InitGPIO();
	InitTimers();
	startTPMx(0,1);
	InitADCs();
	InitUARTs();
	
	enablePIT();
	disablePITx(1);
	
	initialize_ui();
	initialize_file_system();
	print_ui();
	WriteServo(180);
	Fsm();
	return 0;
	
}



