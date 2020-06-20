#pragma once

#include <fstream>
#include <string>
#include <iostream>
#include <stdlib.h>

using namespace std;

bool phaseOneExtraction(string*&);		// The function that is called by the main program
bool findTableOne(ifstream&);				// Helper function that searches for the id named "table1." Discards previous inputs
bool findTBody(ifstream&);					// Finds the <tbody> tag. Discards all previous inputs
bool textExtraction(ifstream&, string*&);				// Copies into an output file the contents of the file until </tbody> is reached

/* Dev Notes:
	
	Summary: Phase one extraction entails getting the most important HTML and putting it into a more usable form. 

	To make implementation tighter, I might place these into a class with appropriate private tags, but that really depends. 

	FIXME!!!: This program will not use stdlib.h exit() because this code will eventually get ported to a Unity environment!!! It's not good if an extraction error pops up and crashes Unity!


*/
