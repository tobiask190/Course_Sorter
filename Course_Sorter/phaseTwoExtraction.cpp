#include "phaseTwoExtraction.h"

bool tableRowExtraction(vector< vector<string*> >& courses) {
	
	unsigned courseIndex = 0;
	string temp;

	ifstream inFS;
	inFS.open("filteredHTML.txt");

	if (inFS.is_open() != true) {
		std::cout << "Error opening \"filteredHTML.txt\"" << endl;
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
	int linkIndex;
	int linkEnd;
	bool linksExist = true;

	for (int i = 0; i < course.size(); i++) {
		if (i == 8) {
			// Some weird infinite looping is occurring in this section. Be sure to look at it. 
			std::cout << "Entering debug" << endl;
			std::cout << std::string::npos << endl;
			std::cin.get();
			while (course.at(i)->find('<') != std::string::npos) {
				
				std::cout << "Get initial indices" << endl;
				std::cin.get();
				leftCarrot = course.at(i)->find('<');
				rightCarrot = course.at(i)->find('>') + 1;
				linkIndex = course.at(i)->find("<a");
				std::cout << linkIndex << endl;
				if (linkIndex == std::string::npos) {
					linksExist = false;
				}

				std::cout << "Linke Exists: " << linksExist << endl;

				if (rightCarrot < linkIndex || linksExist == false) { 
					std::cout << "Deleting indices" << endl;
					std::cin.get();
					course.at(i)->insert(rightCarrot, 1, '/');
					course.at(i)->erase(leftCarrot, rightCarrot - leftCarrot);
					std::cout << "Deleted! : " << *course.at(i) << endl;
					std::cin.get();
				}
				else {
					std::cout << "First get indices of link and then delete" << endl; 
					std::cin.get();
					linkIndex = course.at(i)->find("href=");
					linkIndex += 6;
					course.at(i)->erase(leftCarrot, linkIndex - leftCarrot);
					linkEnd = course.at(i)->find('\"');
					rightCarrot = course.at(i)->find('>') + 1;
					course.at(i)->insert(rightCarrot, 1, '/');
					course.at(i)->erase(linkEnd, rightCarrot - linkEnd);
					std::cout << "Deleted! : " << *course.at(i) << endl;
					std::cin.get();
				}
				
				std::cout << "Iteration done" << endl;
				std::cin.get();
			
			}
			std::cout << *course.at(i) << endl;
			std::cin.get();
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