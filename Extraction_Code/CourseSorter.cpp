// Class_Sorter.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "courseObject.h"
//#include "include/SDL.h"

using namespace std;

int main(int argc, char *argv[])
{
	//if (SDL_Init(SDL_INIT_EVERYTHING) < 0) {
	//	cout << "SDL could not initialise!" << endl;
	//}

	vector<Course*> addedCourses;
	if (fullCourseExtraction(addedCourses) == true) {
		for (unsigned i = 0; i < addedCourses.size(); i++) {
			cout << "Course " << i + 1 << endl;
			addedCourses.at(i)->printAll();
			cout << endl;
		}
	}
	else {
		cout << "Something went wrong" << endl;
	}

	return 0;
}


// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages


// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
