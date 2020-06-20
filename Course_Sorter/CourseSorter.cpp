// Class_Sorter.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "phaseOneExtraction.h"
#include "phaseTwoExtraction.h"
#include "courseObject.h"

using namespace std;

int main()
{
	cout << "START OF TESTING" << endl << endl;

	cout << "Create string pointer" << endl;
	string* htmlTable = new string;

	cout << "Create course pointer table" << endl;
	vector<Course*> courseList;
	
	cout << "Begin phase one extraction" << endl;
	if (phaseOneExtraction(htmlTable)) {
		cout << "Success!" << endl;
	}
	else {
		cout << "Something went wrong in phase one." << endl;
	}

	cout << "Press enter to get enter phase two" << endl;
	cin.get();
	//phaseTwoExtraction(htmlTable, courseList);

	if (phaseTwoExtraction(htmlTable, courseList)) {
		cout << "Phase two succesful!" << endl;
		for (unsigned i = 0; i < courseList.size(); i++) {
			cout << "Course " << i << endl;
			courseList.at(i)->printAll();
			cout << endl;
		}
	}
	else {
		cout << "Something went wrong in phase two." << endl;
	}

	return 1;
}


// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages


// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
