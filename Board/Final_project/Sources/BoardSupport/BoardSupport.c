#include "TFC.h"
#include "mcg.h"

#define SERVO_MUDULO_REGISTER   0x9275 - 1 
#define TRIGGER_MODULO_REGISTER 0xC350 - 1 // 50,000

//-----------------------------------------------------------------
// set I/O for switches and LEDs
//-----------------------------------------------------------------
void InitGPIO() {
	//enable Clocks to all ports - page 206, enable clock to Ports
	SIM_SCGC5 |=
			SIM_SCGC5_PORTA_MASK | SIM_SCGC5_PORTB_MASK | SIM_SCGC5_PORTC_MASK
					| SIM_SCGC5_PORTD_MASK | SIM_SCGC5_PORTE_MASK;

	/////////// LEDs /////////////
	PORTD_PCR1 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK;  //Blue
	GPIOD_PDDR |= BLUE_LED_LOC; //Setup as output pin	
	PORTB_PCR18 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK; //Red  
	PORTB_PCR19 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK; //Green
	GPIOB_PDDR |= RED_LED_LOC + GREEN_LED_LOC; //Setup as output pins

	RGB_LED_OFF;

	//////// Pushbuttons ///////////
	PORTD_PCR7 = PORT_PCR_MUX(1); // assign PTD7 as GPIO
	GPIOD_PDDR &= ~PORT_LOC(7);  // PTD7 is Input
	PORTD_PCR7 |= PORT_PCR_PS_MASK | PORT_PCR_PE_MASK | PORT_PCR_PFE_MASK | PORT_PCR_IRQC(0x0a);

	PORTD_PCR6 = PORT_PCR_MUX(1); // assign PTD6 as GPIO
	GPIOD_PDDR &= ~PORT_LOC(6);  // PTD7 is Input
	PORTD_PCR6 |= PORT_PCR_PS_MASK | PORT_PCR_PE_MASK | PORT_PCR_PFE_MASK | PORT_PCR_IRQC(0x0a);

	enable_irq(INT_PORTD - 16); // Enable Interrupts 
	set_irq_priority(INT_PORTD - 16, 0);  // Interrupt priority = 0 = max

	//////// LCD ///////////
	PORTE_PCR3 = PORT_PCR_MUX(1); // LCD - RS
	PORTE_PCR4 = PORT_PCR_MUX(1); // LCD - R/w
	PORTE_PCR5 = PORT_PCR_MUX(1); // LCD - ENABLE

	PORTB_PCR0 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR1 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR2 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR3 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT

	GPIOB_PDDR |= 0xF; // Setup LCD DATA pins as output

	lcd_init(); // init the LCD
	
	/////// Servo /////////////////
	
	PORTA_PCR12 = PORT_PCR_MUX(3); // TPM1_CH0 - ALT3

	/////// Ultra-sonic sensor/////
	// Trigger
	PORTE_PCR22 = PORT_PCR_MUX(3); // TPM1_CH1- ALT3
	//pit
	PORTC_PCR7  = PORT_PCR_MUX(1); // set GPIO
	GPIOC_PDDR  |= PORT_LOC(7);    // PTC7 is output
	// Echo
	PORTE_PCR29  = PORT_PCR_MUX(3); // TPM0_CH2 - ALT3
}

//-----------------------------------------------------------------
// TPMx - Clock Setup
//-----------------------------------------------------------------
void ClockSetup() {

	pll_init(8000000, LOW_POWER, CRYSTAL, 4, 24, MCGOUT); //Core Clock is now at 48MHz using the 8MHZ Crystal

	//Clock Setup for the TPM requires a couple steps.
	//1st,  set the clock mux
	//See Page 124 of f the KL25 Sub-Family Reference Manual
	SIM_SOPT2 |= SIM_SOPT2_PLLFLLSEL_MASK;// We Want MCGPLLCLK/2=24MHz (See Page 196 of the KL25 Sub-Family Reference Manual
	SIM_SOPT2 &= ~(SIM_SOPT2_TPMSRC_MASK);
	SIM_SOPT2 |= SIM_SOPT2_TPMSRC(1); //We want the MCGPLLCLK/2 (See Page 196 of the KL25 Sub-Family Reference Manual
	//Enable the Clock to the TPM0 and PIT Modules
	//See Page 207 of f the KL25 Sub-Family Reference Manual
	SIM_SCGC6 |= SIM_SCGC6_TPM0_MASK + SIM_SCGC6_TPM1_MASK + SIM_SCGC6_TPM2_MASK;
	// TPM_clock = 24MHz 

}

//-----------------------------------------------------------------
// TPMx - Initialization
//-----------------------------------------------------------------
void InitTPMx(char x){  // x={0,1,2}
	switch(x){ 
	case 0: // Echo
		TPM0_SC = 0; // to ensure that the counter is not running
		TPM0_SC |= TPM_SC_PS(3); //Prescaler 8
		// TPM period = (MOD + 1) * CounterClock_period
		TPM0_MOD = 0xFFFF;//SERVO_MUDULO_REGISTER; // PWM frequency of 40Hz = 24MHz/(8x60,000)
		TPM0_C2SC = 0;
		// Input capture both edge detect
		TPM0_C2SC |= TPM_CnSC_ELSB_MASK + TPM_CnSC_ELSA_MASK + TPM_CnSC_CHIE_MASK;
		TPM0_CONF = 0;
		enable_irq(INT_TPM0-16); // Enable Interrupts 
		set_irq_priority (INT_TPM0-16,0);  // Interrupt priority = 0 = max
		break;
		
	case 1: // Servo
		TPM1_SC = 0; // to ensure that the counter is not running
		TPM1_SC |= TPM_SC_PS(4)+TPM_SC_TOIE_MASK; //Prescaler = 16, up-mode, counter-disable
		// TPM period = (MOD + 1) * CounterClock_period
		TPM1_MOD = SERVO_MUDULO_REGISTER; // PWM frequency of 40Hz = 24MHz/(16x60,000)
		TPM1_C0SC = 0;
		// Edge Aligned , High-True pulse, channel interrupts enabled
		TPM1_C0SC |= TPM_CnSC_MSB_MASK + TPM_CnSC_ELSB_MASK + TPM_CnSC_CHIE_MASK;
		TPM1_C0V = TPM_DC_VAL_MIN; // Duty Cycle 5% - servo deg = 0
		TPM1_CONF = 0;//TPM_CONF_DBGMODE(3); //LPTPM counter continues in debug mode
		
		break;
	case 2: // Trigger
		TPM2_SC = 0; // to ensure that the counter is not running
		TPM2_SC |= TPM_SC_PS(5) + TPM_SC_TOIE_MASK;  //Prescaler = 32, clear flag
		// TPM period = (MOD + 1) * CounterClock_period
		TPM2_MOD = TRIGGER_MODULO_REGISTER;  // PWM frequency of 15Hz = 24MHz/(32x50,000)
		TPM2_C0SC = 0;
		// Edge Aligned , High-True pulse, channel interrupts enabled
		TPM2_C0SC |= TPM_CnSC_MSB_MASK + TPM_CnSC_ELSB_MASK + TPM_CnSC_CHIE_MASK;
		TPM2_C0V = 10; // Duty Cycle( > 10us)
		TPM2_CONF = 0;//TPM_CONF_DBGMODE(3); //LPTPM counter continues in debug mode
		break;
	}
		
}

//-----------------------------------------------------------------
// TPMx - Set Duty Cycle (FROM PWM MODE)
//-----------------------------------------------------------------
void SetTPMxDutyCycle(char x, int dutyCycle){
	
	switch(x){
	case 0:
		break;
	case 1:
		TPM1_C0V = dutyCycle; 
		break;
	case 2:
		TPM2_C1V = dutyCycle; 
		break;
	}
}
//-----------------------------------------------------------------
// Start TPMx
//-----------------------------------------------------------------
void StartTPMx(char x, int start){
	switch(x){
	case 0:
		if(start)
			TPM0_SC |= TPM_SC_CMOD(1); //Start the TPM0 counter
		else
			TPM0_SC &= ~TPM_SC_CMOD(1); //Stop the TPM0 counter
		break;
	case 1:
		if(start)
			TPM1_SC |= TPM_SC_CMOD(1); //Start the TPM1 counter
		else 
			TPM1_SC &= ~TPM_SC_CMOD(1); //Stop the TPM1 counter
		break;
	case 2:
		if(start)
			TPM2_SC |= TPM_SC_CMOD(1); //Start the TPM2 counter
		else
			TPM2_SC &= ~TPM_SC_CMOD(1); //Stop the TPM2 counter
		break;
	}
	
}
void clearTPM0(){
	TPM0_CNT = 0;
}
//-----------------------------------------------------------------
// TPMx - Clock Setup
//-----------------------------------------------------------------
void ClockSetupTPM(){
	    
	pll_init(8000000, LOW_POWER, CRYSTAL,4,24,MCGOUT); //Core Clock is now at 48MHz using the 8MHZ Crystal
	
	//Clock Setup for the TPM requires a couple steps.
	//1st,  set the clock mux
	//See Page 124 of f the KL25 Sub-Family Reference Manual
	SIM_SOPT2 |= SIM_SOPT2_PLLFLLSEL_MASK;// We Want MCGPLLCLK/2=24MHz (See Page 196 of the KL25 Sub-Family Reference Manual
	SIM_SOPT2 &= ~(SIM_SOPT2_TPMSRC_MASK);
	SIM_SOPT2 |= SIM_SOPT2_TPMSRC(1); //We want the MCGPLLCLK/2 (See Page 196 of the KL25 Sub-Family Reference Manual
	//Enable the Clock to the TPM0 and PIT Modules
	//See Page 207 of f the KL25 Sub-Family Reference Manual
	SIM_SCGC6 |= SIM_SCGC6_TPM0_MASK + SIM_SCGC6_TPM1_MASK + SIM_SCGC6_TPM2_MASK;
	// TPM_clock = 24MHz	    
}

//-----------------------------------------------------------------
// PIT - Initialisation
//-----------------------------------------------------------------
void InitPIT() {
	// PIT_clock = 24MHz
	SIM_SCGC6 |= SIM_SCGC6_PIT_MASK; //Enable the Clock to the PIT Modules
	// Timer 0
	PIT_LDVAL0 = 0x00186A00; // setup timer 0 for 15hz counting period

	// Timer 1
	PIT_LDVAL1 = 0x000912C0; // setup timer 0 for 10msec counting period

	PIT_MCR |= PIT_MCR_FRZ_MASK; // stop the pit when in debug mode

	enable_irq(INT_PIT - 16); //  //Enable PIT IRQ on the NVIC
	set_irq_priority(INT_PIT - 16, 0);  // Interrupt priority = 0 = max
}


/*
 * Sets PIT's counter 
 */
void SetPITxInterval(int x, unsigned int ms) {
	unsigned int interval = ms*24000;
	switch (x){
	case (0): PIT_LDVAL0 = interval; break;
	case (1): PIT_LDVAL1 = interval; break;
	}
}

/*
 * Enable  / Disable PIT Module
 */
void EnablePITModule(int enable) {
	if(enable)
		PIT_MCR &= ~PIT_MCR_MDIS_MASK; // Enables the PIT module
	else
		PIT_MCR |= PIT_MCR_MDIS_MASK; // Disables the PIT module
}

/*
 * Enable/Disable PIT x
 */
void enablePITx(int x, int enable) {
	if (enable){
		if (x == 1) {
			PIT_TCTRL1 |= PIT_TCTRL_TIE_MASK | PIT_TCTRL_TEN_MASK;
		} else if (x == 0) {
			PIT_TCTRL0 |= PIT_TCTRL_TIE_MASK | PIT_TCTRL_TEN_MASK;
		}
	} else {
		if (x == 1) {
			PIT_TCTRL1 &= ~(PIT_TCTRL_TIE_MASK | PIT_TCTRL_TEN_MASK);
		} else if (x == 0) {
			PIT_TCTRL0 &= ~(PIT_TCTRL_TIE_MASK | PIT_TCTRL_TEN_MASK);
		}
	}
}



