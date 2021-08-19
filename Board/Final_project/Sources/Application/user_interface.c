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
//                            Title           StateName       enter          scroll        print        init       #submenus
Menu radar_detection_menu = {"1.Radar detect",RADAR_DETECT_E ,return_command,no_action    ,no_action   ,no_action  ,0};
Menu telemeter_menu       = {"2.Telemeter   ",TELEMETER_E    ,return_command,no_action    ,no_action   ,no_action  ,0};
Menu script_menu          = {"3.Script      ",SCRIPT_E       ,script_enter  ,script_scroll,script_print,init_script,0};
Menu configuration_menu   = {"4.Config      ",CONFIGURATION_E,gen_enter     ,gen_scroll   ,gen_print   ,no_action  ,2,{&main_menu,&baud_menu}};
//Menu back_menu            = {"<-Back        ",IDLE_E         ,return_command,no_action    ,no_action   ,no_action  ,0};
Menu baud_menu            = {"Baud= 9600    ",IDLE_E         ,return_command,no_action    ,no_action   ,no_action  ,0};
Menu main_menu =            {"<-Main menu   ",IDLE_E         ,gen_enter     ,gen_scroll   ,gen_print   ,no_action  ,4 ,{&radar_detection_menu,&telemeter_menu,&script_menu,&configuration_menu}};


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

StateModes script_enter() {}
void       script_scroll(){
	if (line_select != menu.num_submenus-1) {
		last_file_select = file_select;
		file_select = file_index_plusplus(file_select); // -1 is for back
		last_file_descriptor = current_file_desc;
		current_file_desc = file_info(file_select);
	}
	line_select = get_next_line(line_select);
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

//void chat_action(){
//	char* line = getChatLine(line_select);
//	send2pc(TYPE.TEXT,line);
//}
//
//void read_action(){
//	read_file_init(last_file_select);
//	read_line();
//	copy_16chars(last_read_line, reading_Line);
//	read_line();
//	copy_16chars(current_read_line, reading_Line);
//}
//
//void send_file_action(){
//	send_file2pc(last_file_select);
//}


/////////////////////////////////
//		Get Next Line
////////////////////////////////
int get_next_line(int line,int menu_size){
	if (line >= menu_size-1){
		return 0;
	}
	return line+1;
}

