#include "phaseOneExtraction.h"

bool extractCourseLine(const string& inputFileName) {

	bool located;

// opening both input and output files
	ifstream inFS;
	inFS.open(inputFileName.c_str());

	if (!inFS.is_open()) {
		
		cout << "Error opening " << inputFileName;

		return false;
	
	}

	//Phase 1: search until you find the id "table1"
	located = findTableOne(inFS);
	
	//Check to see if id table1 was found at all. 
	if (located == false) {
		cout << "table1 not found." << endl;
		inFS.close();
		return false;
	}
	else {
		cout << "table1 found" << endl;
	}
	
	//Phase 2: search until you find the <tbody> tag
	located = false;
	located = findTBody(inFS);

	if (located == false) {
		cout << "tbody not found.";
		inFS.close();
		return false;
	}
	else {
		cout << "tbody found" << endl;
	}

	//Phase 3: Extract all the text until </tbody> appears
	if (textExtraction(inFS)) {
		cout << "Extraction complete" << endl;
	}
	else {
		cout << "An error occurred" << endl;
	}

	

	cout << "End of test" << endl;
	inFS.close();

	return true;
}

bool findTableOne(ifstream &inFS) {

	string testString;
	char tempChar;
	bool foundID = false;

	// Subphase 1: Find the HTML tag (<tag>) 
	while (inFS >> tempChar) {

		//find the iconic start of a tag
		if (tempChar == '<') {

			// Subphase 2: Find the "id=", stop if you reach '>'
			while (tempChar != '>' && foundID != true) {

				// One big long chain to see if it's id=...
				inFS >> tempChar;
				if (tempChar == 'i') {
					inFS >> tempChar;
					if (tempChar == 'd') {
						inFS >> tempChar;
						if (tempChar == '=') {
							// Subphase 3: Check if the label is "table1"
							foundID = true;
							inFS >> tempChar; // Remove leading " character
							getline(inFS, testString, '"');
							if (testString == "table1") {
								return true;
							}
						}
					}
				}

			}

			foundID = false;

		}

	}

	return false;


}

bool findTBody(ifstream& inFS) {
	
	char tempChar;
	string tempString;

	while (inFS >> tempChar) {
		
		if (tempChar == '<') {
			getline(inFS, tempString, '>');

			if (tempString == "tbody") {
				return true;
			}
		}

	}

	return false;
}

bool textExtraction(ifstream& inFS) {
	
	char tempChar;
	string tempString;

	ofstream outFS;
	outFS.open("filteredHTML.txt");

	if (!outFS.is_open()) {

		inFS.close();

		cout << "Error opening " << "filteredHTML.txt";
		return false;

	}


	while (inFS >> tempChar) {

		if (tempChar == '<') {
			getline(inFS, tempString, '>');
			
			if (tempString == "/tbody") {
				outFS.close();
				return true;
			}
			else if (tempString.find("tr ") != std::string::npos) {
				outFS << '<' << tempString << '>' << endl;
			}
			else if (tempString == "/tr") {
				outFS << '<' << tempString << '>' << endl << endl;
			}
			else if (tempString == "/td") {
				outFS << '<' << tempString << '>' << endl;
			}
			else {
				outFS << '<' << tempString << '>';
			}

		}
		else {
			outFS << tempChar;
		}

	}

	outFS.close();

	return false;

}