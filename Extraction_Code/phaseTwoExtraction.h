#pragma once

#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <vector>
#include "courseObject.h"

using namespace std;

bool phaseTwoExtraction(string*&, vector<Course*>&);
bool tableRowExtraction(string*&, vector< vector<string*> >&);		// Takes a string pointer vector as input and places each bracketed <tr> line into a separate string
bool tagScrubber(vector<string*>& course);