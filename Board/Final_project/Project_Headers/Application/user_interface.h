/*
 * user_interface.h
 *
 *  Created on: Aug 9, 2021
 *      Author: tomer
 */

#ifndef USER_INTERFACE_H_
#define USER_INTERFACE_H_

#define MAX_SUBMENUS  20
#define MAX_TITLE 17
#define CONFIGURATION_MENU_SIZE 2
#define SCAN_DELAY 50

typedef struct Menu{
	char       title[MAX_TITLE];
	StateModes  menu_state;
	StateModes (*enter_callback)();
	void	   (*scroll_down_callback)();
	void       (*print_callback)();
	void       (*initialize_menu)();
	short num_submenus;
	struct Menu *submenu[MAX_SUBMENUS];
}Menu;
Menu menu;

extern Menu radar_detection_menu;
extern Menu telemeter_menu;
extern Menu script_menu;
extern Menu configuration_menu;
extern Menu back_menu;
extern Menu baud_menu;
extern Menu main_menu;




int line_select;
int file_select;
int last_file_select;

char last_read_line[16];
char current_read_line[16];
extern const char back[16];

File_descriptor *last_file_descriptor;
File_descriptor *current_file_desc;

void print_ui();
void scroll_down();
StateModes enter();
char* build_scan_msg(char* msg, int dis, int deg);
StateModes rad_detect_sys();
StateModes telemeter_system();
#endif /* USER_INTERFACE_H_ */
