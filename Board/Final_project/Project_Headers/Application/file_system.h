/*
 * file_system.h
 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */

#ifndef FILE_SYSTEM_H_
#define FILE_SYSTEM_H_

#define SIZE_OF_FILE_SYSTEM 10000
#define MAX_NUMBER_OF_FILES 3
#define MAX_NAME_SIZE       20
#define LINE_LENGTH         16
#define FILE_SYSTEM_START_ADDRESS fs_memory
#define FILE_SYSTEM_END_ADDRESS fs_memory + SIZE_OF_FILE_SYSTEM



// the file system's memory
extern char     fs_memory[SIZE_OF_FILE_SYSTEM];
extern char     reading_Line[LINE_LENGTH+1];

int first_time;

// File System States
typedef enum FS_StateModes{IDLE_FS,READ_FILE_FS,WRITE_SIZE_FS,WRITE_DATA_FS} FS_StateModes;

typedef struct File_descriptor{
	char  name[20];
	char* start_pointer;
	int   size;
	int   valid;
}File_descriptor;

typedef struct File_System{
	int             number_of_files; // when larger than zero there are files in the file_system
	File_descriptor file_list[MAX_NUMBER_OF_FILES];
	FS_StateModes   state;
	int             first_file;
	int             last_file;
	int             system_size_remaining;
	
	// write operations
	char*           write_pointer;
	File_descriptor temp_file_desc;
	int             size_remaining; // when larger than 0 there is a write in operation

	
	// send to pc
	int   send_file;
	char* send_pointer; 
	
	// read to lcd operations
	int   read_file;
	int   number_of_bytes_read;
	char* read_pointer;   // not sure this is necessary
	
	// constants
	int*   start_address;
	int*   end_address;
} File_System;

File_System file_system;

void             initialize_file_system();
File_descriptor* file_info(int index);
int 			 read_file_init(int file_num);
int              read_line(); // reads up to 16 chars or EOF
void             read_commandline_init(int file_index);
int              read_commandline(char* new_line);
int              write_file_init_message(char* message);
int				 write_file_chunck(char* write_data, int size);
int              remove_last_file();
int              file_index_plusplus_with_menu(int file_index);
int              file_index_plusplus(int file_index);
int              file_index_minusminus(int file_index);


#endif /* FILE_SYSTEM_H_ */
