/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
  
int distance_ready = 0;
unsigned int distance;
volatile float rising_edge = 0;
volatile float falling_edge = 0;
volatile unsigned int signal_taken = 0;	  

int main(void){
	
	InitGPIO();
	InitTimers();
	InitUARTs();
	
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



