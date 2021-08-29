/*
 * main implementation: use this 'C' sample to create your own application
 *
 */

# include "TFC.h"
  
volatile int distance_ready = 0;
unsigned int distance;
volatile float rising_edge = 0;
volatile float falling_edge = 0;
volatile unsigned int signal_taken = 0;	  

int main(void){
	
	// Init board components
	InitGPIO();
	InitTimers();
	InitUARTs();
	EnablePITModule(TRUE);

	// Init additional components
    InitServo();
    InitSensors();
    enable_sensor(FALSE);
    
    // Init UI
    init_hal();
	initialize_ui();
	initialize_file_system();
	print_ui();
	
	Fsm();
	
	return 0;
	
}



