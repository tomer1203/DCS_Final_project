/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable
int add(int a, int b);
int main(void){
	
	char  s[100] = {0};
	int i=0;
	int (*function_pointer)(int,int)= add;
	sprintf(s,"%d",function_pointer(2,3));
	ClockSetup();
	InitGPIO();
	Print(s);

	InitPIT();
	InitADCs();
	InitUARTs();
	enablePIT();
	disablePITx(1);
	
	initialize_ui();
	initialize_file_system();
	print_ui();
//	
	Fsm();
	return 0;
	
}
int add(int a,int b){
	return a+b;
}


