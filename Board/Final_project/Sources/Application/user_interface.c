/*
 * user_interface.c
 *
 *  Created on: Aug 9, 2021
 *      Author: tomer
 */
# include "TFC.h"
const char back[16] = "<=Back";

StateModes return_command();
void       no_action();
StateModes gen_enter();
StateModes script_enter();
void       script_scroll();
void       script_print();
void       gen_scroll();
void       gen_print();
void       init_script();
int        get_next_line(int line,int menu_size);
//                            Title           StateName       enter            scroll        print        init       #submenus
Menu radar_detection_menu = {"1.Radar detect",RADAR_DETECT_E ,rad_detect_sys  ,no_action    ,no_action   ,no_action  ,0}; 
Menu telemeter_menu       = {"2.Telemeter   ",TELEMETER_E    ,telemeter_system,no_action    ,no_action   ,no_action  ,0};
Menu script_menu          = {"3.Script      ",SCRIPT_E       ,script_enter    ,script_scroll,script_print,init_script,1}; // Script
Menu configuration_menu   = {"4.Config      ",CONFIGURATION_E,gen_enter       ,gen_scroll   ,gen_print   ,no_action  ,2,{&main_menu,&baud_menu}};
//Menu back_menu            = {"<-Back        ",IDLE_E         ,return_command,no_action    ,no_action   ,no_action  ,0};
Menu baud_menu            = {"Baud= 9600    ",IDLE_E         ,return_command  ,no_action    ,no_action   ,no_action  ,0};
Menu main_menu =            {"<-Main menu   ",IDLE_E         ,gen_enter       ,gen_scroll   ,gen_print   ,no_action  ,4 ,{&radar_detection_menu,&telemeter_menu,&script_menu,&configuration_menu}};


void copy_16chars(char* to, char* from) {
	int i = 0;
	for (i = 0;i < 16; i++) {
		to[i] = from[i];
	}
}

void initialize_ui(){
	line_select = 0;
	menu = main_menu;
}


/////////////////////////////////
//		Print UI
////////////////////////////////
void print_ui(){
	menu.print_callback();
}


/////////////////////////////////
//		Scroll Down
////////////////////////////////
void scroll_down(){
	menu.scroll_down_callback();
}

/////////////////////////////////
//		Enter
////////////////////////////////
StateModes enter(){
	StateModes next_state = menu.enter_callback();
	return next_state;
	
} // END enter

/////////////////////////////////
//		CALLBACK FUNCTIONS!
////////////////////////////////

StateModes return_command(){
	menu = main_menu;
	return IDLE_E;
}
void       no_action(){}

StateModes gen_enter(){
	if (menu.submenu[line_select]->num_submenus == 0){
		menu.submenu[line_select]->enter_callback();
	} else {
		menu = *menu.submenu[line_select];
		menu.initialize_menu();
		line_select = 0;
		return menu.menu_state;	
	}
	
}

void       gen_scroll(){
	line_select = get_next_line(line_select,menu.num_submenus);
}
void       gen_print(){
	Print_two_lines(menu.submenu[line_select]->title,
			        menu.submenu[get_next_line(line_select,menu.num_submenus)]->title);
}
char* build_scan_msg(char* msg, int dis, int deg){
	char dis_ascii[16];
	char Angle[4] = {0};
	char Angle_padded[4] = {0};
	
	sprintf(dis_ascii,"%d",dis);
	sprintf(Angle,"%d",deg);
	if (deg < 10){
		strcpy(Angle_padded, "00");
		strcat(Angle_padded,Angle);
	} else if (deg < 100){
		strcpy(Angle_padded, "0");
		strcat(Angle_padded,Angle);
	}else{
		strcpy(Angle_padded,Angle);
	}
	strcpy(msg,Angle_padded);
	strcat(msg,dis_ascii);
	return msg;
}
StateModes rad_detect_sys(){
	int degree = 0;
	char msg[20] = {0};
	enterON = FALSE;
	enable_sensor(TRUE);
	while (1){
		WriteServo(degree);
		degree += SERVO_DEG_CHANGE;
		if (degree > 180){
			degree = 0;
		}
		while(!distance_ready){
			WaitDelay(10);
		}
		if (distance_ready){
			build_scan_msg(msg,out_distance,degree);
			send2pc("Sc",msg);
			Print(msg);
			distance_ready = FALSE;
		}
		if (enterON || stopRadar){
			enterON = FALSE;
			enable_sensor(FALSE);
			return state;
		}
		WaitDelay(200);//50
	}
}
StateModes telemeter_system(){
	char str[16];
	enterON = FALSE;
	enable_sensor(TRUE);
	while(1){
		if (distance_ready){
			sprintf(str,"%d",out_distance);
			send2pc("Te",str);
			Print(str);
			distance_ready = FALSE;
		}
		if (enterON || stopRadar){
			enterON = FALSE;
			enable_sensor(FALSE);
			return state;
		}
		WaitDelay(50);
	}
}
void parse_command(int* command_p, int* arg1_p, int* arg2_p, char* command_line){
	char Temp[3] = {0};
	
	if(command_line[0] == '\0')return;
	Temp[0] = command_line[0];
	Temp[1] = command_line[1];
	*command_p = (int)strtol(Temp, NULL, 16);
	
	if(command_line[2] == '\0')return;
	Temp[0] = command_line[2];
	Temp[1] = command_line[3];	
	*arg1_p = (int)strtol(Temp, NULL, 16);
	
	if(command_line[4] == '\0')return;
	Temp[0] = command_line[4];
	Temp[1] = command_line[5];
	*arg2_p = (int)strtol(Temp, NULL, 16);
}
StateModes script_enter() {
	char  new_line[10];
	int command,arg1,arg2;
	int* command_p,arg1_p,arg2_p;
	if (line_select == 0){
		menu = main_menu;
		return IDLE_E;
	}
	command_p = &command;
	arg1_p = &arg1;
	arg2_p = &arg2;
	read_commandline_init(file_select);
	
	while (read_commandline(new_line)){
		parse_command(command_p,arg1_p,arg2_p,new_line);
		switch(command){
		case(1):blink_rgb(arg1);break;
		case(2):lcd_count_up(arg1);break;
		case(3):lcd_count_down(arg1);break;
		case(4):set_delay(arg1);break;
		case(5):clear_all_leds();break;
		case(6):servo_deg(arg1);break;
		case(7):servo_scan(arg1,arg2);break;
		case(8):sleep();break;
		}		
	}
	return SCRIPT_E;
}
void       script_scroll(){
	if (line_select != menu.num_submenus-1) {
		last_file_select = file_select;
		file_select = file_index_plusplus(file_select); // -1 is for back
		last_file_descriptor = current_file_desc;
		current_file_desc = file_info(file_select);
	}
	line_select = get_next_line(line_select, menu.num_submenus);
}
void       script_print() {
	if (line_select == 0) {
		Print_two_lines(back, current_file_desc->name);
	}
	else if (line_select == menu.num_submenus-1) {
		Print_two_lines(last_file_descriptor->name, back);
	}
	else
		Print_two_lines(last_file_descriptor->name,current_file_desc->name);
}
void init_script(){
	file_select = file_system.first_file;
	line_select = 0;
	if (file_system.number_of_files == 0) {
		menu.num_submenus = 0;
	} else {
		menu.num_submenus = file_system.number_of_files+1;
	}
	current_file_desc = file_info(file_select);
}
////////////////////////////////
//		Actions
////////////////////////////////

/////////////////////////////////
//		Get Next Line
////////////////////////////////
int get_next_line(int line,int menu_size){
	if (line >= menu_size-1){
		return 0;
	}
	return line+1;
}

