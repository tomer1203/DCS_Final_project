/*
 * main implementation: use this 'C' sample to create your own application
 *
 */

# include "TFC.h"

// Declarations of externs
volatile int distance_ready = FALSE;
volatile int signal_taken = FALSE;	
volatile unsigned int acc_distance = 0;
volatile unsigned int out_distance = 0;
volatile unsigned int distance = 0;
volatile unsigned int rising_edge = 0;
volatile unsigned int falling_edge = 0;


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



