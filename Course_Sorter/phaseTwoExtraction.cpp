#include "phaseTwoExtraction.h"

bool phaseTwoExtraction(string*& htmlTable, vector<Course*>& courseList) {

	// Create the string version of the course elements with tags included
	// Each element of the parent vector is an individual course 
	vector< vector<string*> > courseListString;
	
	// Begin to insert the elements from the HTML table into course-element order
	if (!tableRowExtraction(htmlTable, courseListString)) {
		cout << "Error occured in tableRowExtraction" << endl;
		return false;
	}

	// Remove from ALL elements the tags (One of the elements will have additional markings)
	for (unsigned courseIndex = 0; courseIndex < courseListString.size(); courseIndex++) {
		if (!tagScrubber(courseListString.at(courseIndex))) {
			cout << "Something went wrong with tab scrubbing" << endl;
			return false;
		}
	}

	// Resize the course to the number of courses in the courseListString
	courseList.resize(courseListString.size());

	//Generate course objects and push to output
	for (unsigned courseIndex = 0; courseIndex < courseListString.size(); courseIndex++) {
		courseList.at(courseIndex) = new Course(courseListString.at(courseIndex));
	}

	//Deallocating the string pointers to prevent memory leaks
	for (unsigned i = 0; i < courseListString.size(); i++) {
		for (unsigned j = 0; j < courseListString.at(i).size(); j++) {
			delete courseListString.at(i).at(j);
		}
	}

	return true;
}

bool tableRowExtraction(string*& htmlTable, vector< vector<string*> >& courses) {
	
	unsigned courseIndex = 0;
	string tempString;
	stringstream inSS;

	inSS << *htmlTable;

	while (getline(inSS, tempString)) {

		if (tempString.find("<tr ") != std::string::npos) {
			
			courses.push_back(vector<string*> (11));

			for (int i = 0; i < 11; i++) {
				getline(inSS, tempString);
				courses.at(courseIndex).at(i) = new string(tempString);
			}

			while(tempString.find("</tr>")) {
				getline(inSS, tempString);
			}

			courseIndex++;
		}
	}

	return true;

}

bool tagScrubber(vector<string*>& course) {

	int leftCarrot = -1;
	int rightCarrot = -1;
	int linkIndex;
	int linkEnd;

	for (unsigned i = 0; i < course.size(); i++) {

		if (i == 8) {

			while (course.at(i)->find('<') != std::string::npos) {

				leftCarrot = course.at(i)->find('<');
				rightCarrot = course.at(i)->find('>') + 1;
				linkIndex = course.at(i)->find("<a");

				if (course.at(i)->find('<') == std::string::npos) {

				}
				else if (rightCarrot < linkIndex || course.at(i)->find("<a") == std::string::npos) {

					course.at(i)->insert(rightCarrot, 1, '|');
					course.at(i)->erase(leftCarrot, rightCarrot - leftCarrot);

				}
				else {

					linkIndex = course.at(i)->find("href=");
					linkIndex += 6;
					course.at(i)->erase(leftCarrot, linkIndex - leftCarrot);
					linkEnd = course.at(i)->find('\"');
					rightCarrot = course.at(i)->find('>') + 1;
					course.at(i)->insert(rightCarrot, 1, '/');

					course.at(i)->erase(linkEnd, rightCarrot - linkEnd);
				}
			}
		}

		else {

			while (course.at(i)->find('<') != std::string::npos) {

				leftCarrot = course.at(i)->find('<');
				rightCarrot = course.at(i)->find('>') + 1;

				course.at(i)->erase(leftCarrot, rightCarrot - leftCarrot);

			}
		}
	}

	return true;
}