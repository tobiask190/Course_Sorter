#include "phaseTwoExtraction.h"

bool tableRowExtraction(vector< vector<string*> >& courses) {
	
	unsigned courseIndex = 0;
	string temp;

	ifstream inFS;
	inFS.open("filteredHTML.txt");

	if (inFS.is_open() != true) {
		cout << "Error opening \"filteredHTML.txt\"" << endl;
		return false;
	}

	while (getline(inFS, temp)) {
		if (temp.find("<tr ") != std::string::npos) {
			courses.push_back(vector<string*> (11));
			for (int i = 0; i < 11; i++) {
				getline(inFS, temp);
				courses.at(courseIndex).at(i) = new string(temp);
			}
			while(temp.find("</tr>")) {
				getline(inFS, temp);
			}
			courseIndex++;
		}
	}

	inFS.close();

	return true;

}

bool tagScrubber(vector<string*>& course) {
	int leftCarrot = -1;
	int rightCarrot = -1;

	for (int i = 0; i < course.size(); i++) {
		while (course.at(i)->find('<') != std::string::npos) {

			leftCarrot = course.at(i)->find('<');
			rightCarrot = course.at(i)->find('>') + 1;

			course.at(i)->erase(leftCarrot, rightCarrot - leftCarrot);

		}
		
	}

	return true;
}