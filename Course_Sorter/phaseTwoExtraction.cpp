#include "phaseTwoExtraction.h"

bool phaseTwoExtraction(string*& htmlTable, vector<Course*>& courseList) {

	vector< vector<string*> > courseString;
	
	if (!tableRowExtraction(htmlTable, courseString)) { // FIXME: DON'T FORGET TO DEALLOCATE AFTERWARDS!!!
		cout << "Error occured in tableRowExtraction" << endl;
		return false;
	}

	for (unsigned courseIndex = 0; courseIndex < courseString.size(); courseIndex++) {
		if (!tagScrubber(courseString.at(courseIndex))) {
			cout << "Something went wrong with tab scrubbing" << endl;
			return false;
		}
	}

	courseList.resize(0);

	for (unsigned courseIndex = 0; courseIndex < courseString.size(); courseIndex++) {
		courseList.push_back(new Course(courseString.at(courseIndex)));
	}

	cout << "Print the stuff" << endl;
	cin.get();
	for (unsigned i = 0; i < courseList.size(); i++) {
		courseList.at(i)->printAll();
		cout << endl;
	}
	cin.get();

	//Deallocating the string pointers to prevent memory leaks
	for (unsigned i = 0; i < courseString.size(); i++) {
		for (unsigned j = 0; j < courseString.at(i).size(); j++) {
			delete courseString.at(i).at(j);
		}
	}

	return true;
}

bool tableRowExtraction(string*& htmlTable, vector< vector<string*> >& courses) {
	
	unsigned courseIndex = 0;
	string temp;

	//ifstream inFS;
	//inFS.open("filteredHTML.txt");

	//if (inFS.is_open() != true) {
	//	std::cout << "Error opening \"filteredHTML.txt\"" << endl;
	//	return false;
	//}

	stringstream inSS;
	inSS << *htmlTable;

	while (getline(inSS, temp)) {
		if (temp.find("<tr ") != std::string::npos) {
			courses.push_back(vector<string*> (11));
			for (int i = 0; i < 11; i++) {
				getline(inSS, temp);
				courses.at(courseIndex).at(i) = new string(temp);
			}
			while(temp.find("</tr>")) {
				getline(inSS, temp);
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