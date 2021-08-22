#include "TFC.h"
#include "mcg.h"

#define MUDULO_REGISTER  0x9275 - 1 // 18,750 -1

// set I/O for switches and LEDs
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
	GPIOD_PDDR |= PORT_LOC(4);  // PTD4 is output
	PORTD_PCR4 = PORT_PCR_MUX(4); // TPM0_CH1- ALT4
	
	/////// Ultra-sonic sensor/////
	// Trigger
	PORTA_PCR12 = PORT_PCR_MUX(3); // TPM1_CH1- ALT3
	PORTC_PCR7  = PORT_PCR_MUX(1); // set GPIO
	GPIOC_PDDR  |= PORT_LOC(7);  // PTA1 is output
	// Echo
	//GPIOA_PDDR &= ~PORT_LOC(1);  // PTD4 is input
	PORTA_PCR1 = PORT_PCR_MUX(3); // TPM2_CH1- ALT3
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
	SIM_SCGC6 |= SIM_SCGC6_TPM0_MASK + SIM_SCGC6_TPM2_MASK;
	// TPM_clock = 24MHz , PIT_clock = 48MHz

}

//-----------------------------------------------------------------
// TPMx - Initialization
//-----------------------------------------------------------------
void InitTPM(char x){  // x={0,1,2}
	switch(x){
	case 0: // Init TPM 0 Channel 4 for PTD4 (? FTM0_CH4 ?) page 567  
		TPM0_SC = 0; // to ensure that the counter is not running
		TPM0_SC |= TPM_SC_PS(4)+TPM_SC_TOIE_MASK; //Prescaler = 8 / 32, up-mode, counter-disable
		// TPM period = (MOD + 1) * CounterClock_period
		TPM0_MOD = MUDULO_REGISTER; // PWM frequency of 40Hz = 24MHz/(8x60,000)
		TPM0_C4SC = 0;
		// Edge Aligned , High-True pulse, channel interrupts enabled
		TPM0_C4SC |= TPM_CnSC_MSB_MASK + TPM_CnSC_ELSB_MASK + TPM_CnSC_CHIE_MASK;
		TPM0_C4V = TPM_DC_VAL_MIN; // Duty Cycle 5% - servo deg = 0
		TPM0_CONF = 0; 
		break;
	case 1:
		break;
	case 2: 
		// Echo PTA2 TPM2_Ch1
		TPM2_SC = 0; // to ensure that the counter is not running
		TPM2_SC |= TPM_SC_PS(5) + TPM_SC_TOF_MASK;  //Prescaler =32, clear flag
		//24Mhz/32=0.75Mhz, T=1.333us*2^16=0.0873s= overflow time
		TPM2_MOD = 0xFFFF; // value of overflow 
		TPM2_C0SC |= TPM_CnSC_ELSB_MASK + TPM_CnSC_ELSA_MASK + TPM_CnSC_CHIE_MASK + TPM_CnSC_CHF_MASK;//input capture on both rising and falling
		TPM2_CONF |= TPM_CONF_DBGMODE(3); //LPTPM counter continues in debug mode
		enable_irq(INT_TPM2-16); // Enable Interrupts 
		set_irq_priority (INT_TPM2-16,0);  // Interrupt priority = 0 = max
		break;
	}
	
	
	

	
}

//-----------------------------------------------------------------
// TPMx - Set Duty Cycle
//-----------------------------------------------------------------
void SetTPMxDutyCycle(char x, int dutyCycle){
	
	switch(x){
	case 0:
		TPM0_C4V = dutyCycle; 
		break;
	case 1:
		TPM1_C1V = dutyCycle; 
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
			TPM1_SC |= TPM_SC_CMOD(0); //Stop the TPM1 counter
		break;
	case 2:
		if(start)
			TPM2_SC |= TPM_SC_CMOD(1); //Start the TPM2 counter
		else
			TPM2_SC &= ~TPM_SC_CMOD(1); //Stop the TPM2 counter
		break;
	}
	
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
	SIM_SCGC6 |= SIM_SCGC6_TPM0_MASK + SIM_SCGC6_TPM2_MASK;
	    // TPM_clock = 24MHz , PIT_clock = 48MHz
	    
}

//-----------------------------------------------------------------
// PIT - Initialisation
//-----------------------------------------------------------------
void InitPIT() {
	SIM_SCGC6 |= SIM_SCGC6_PIT_MASK; //Enable the Clock to the PIT Modules
	// Timer 0
	PIT_LDVAL0 = 0x00186A00; // setup timer 0 for 15hz counting period
	PIT_LDVAL1 = 0x000912C0; // setup timer 0 for 10msec counting period
	//PIT_TCTRL0 = PIT_TCTRL_TEN_MASK | PIT_TCTRL_TIE_MASK; //enable PIT0 and its interrupt

	// Timer 1
	//PIT_TCTRL1 = PIT_TCTRL_TEN_MASK ;//| PIT_TCTRL_TIE_MASK; //enable PIT0 and its interrupt

	PIT_MCR |= PIT_MCR_FRZ_MASK; // stop the pit when in debug mode

	enable_irq(INT_PIT - 16); //  //Enable PIT IRQ on the NVIC
	set_irq_priority(INT_PIT - 16, 0);  // Interrupt priority = 0 = max
}

/*
 * Sets PIT's counter 
 */
void setPITInterval(unsigned int interval) {
	PIT_LDVAL0 = interval;
}

/*
 * Enable/Disable PIT Module
 */
void enablePIT(int enable) {
	if (enable){
		PIT_MCR &= ~PIT_MCR_MDIS_MASK; // Enables the PIT module	
	} else {
		PIT_MCR |= PIT_MCR_MDIS_MASK; // Disables the PIT module	
	}
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

/*
 * Enables ADC 0 (POT channel is SE0)
 */
void enableADC0() {
	//POT channel is SE0 , ADC interrupt is enabled.
	ADC0_SC1A = POT_ADC_CHANNEL | ADC_SC1_AIEN_MASK;
}

/*
 * Disable ADC 0
 */
void disableADC0() {
	ADC0_SC1A &= ~ADC_SC1_AIEN_MASK;
	// ADC0_SC1B =0x01; 
}
