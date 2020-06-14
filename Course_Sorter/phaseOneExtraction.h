#pragma once

#include <fstream>
#include <string>
#include <iostream>

using namespace std;

bool extractCourseLine(const string&);		// The function that is called by the main program
bool findTableOne(ifstream&);				// Helper function that searches for the id named "table1." Discards previous inputs
bool findTBody(ifstream&);					// Finds the <tbody> tag. Discards all previous inputs
bool textExtraction(ifstream&);				// Copies into an output file the contents of the file until </tbody> is reached

/* Dev Notes:

	To make implementation tighter, I might place these into a class with appropriate private tags, but that really depends. 

*/