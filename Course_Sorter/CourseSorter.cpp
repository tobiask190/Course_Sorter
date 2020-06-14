// Class_Sorter.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "phaseOneExtraction.h"
#include "phaseTwoExtraction.h"
#include "courseObject.h"

using namespace std;

int main()
{
	bool boo;
	//extractCourseLine("input.txt");
	//No longer needed for testing purposes. Will implement later.


	vector< vector<string*> > courses;
	if (!tableRowExtraction(courses)) { // FIXME: DON'T FORGET TO DEALLOCATE AFTERWARDS!!!
		cout << "Error occured in tableRowExtraction" << endl;
		return 0;
	}

	cout << courses.size() << endl;

	for (unsigned courseIndex = 0; courseIndex < courses.size(); courseIndex++) {
		cout << courseIndex << endl;
		boo = tagScrubber(courses.at(courseIndex));
	}
	cout << "tagScrubber successful!" << endl;

	vector<Course> courseObjects(courses.size());

	for (unsigned courseIndex = 0; courseIndex < courseObjects.size(); courseIndex++) {
		courseObjects.at(courseIndex) = Course(courses.at(courseIndex));
	}

	for (unsigned courseIndex = 0; courseIndex < courseObjects.size(); courseIndex++) {
		courseObjects.at(courseIndex).printAll();
		cout << endl;
	}

	//for (unsigned courseIndex = 0; courseIndex < courses.size(); courseIndex++) {
	//	cout << "START OF COURSE" << endl;
	//	for (unsigned elementIndex = 0; elementIndex < courses.at(courseIndex).size(); elementIndex++) {
	//		cout << *courses.at(courseIndex).at(elementIndex) << endl;
	//	}
	//	cout << "END OF COURSE" << endl;
	//}

	//for (int i = 0; i < storage.size(); i++) {
		//cout << *storage.at(i) << endl << endl;
	//}

}


// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages


// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
