#include "phaseOneExtraction.h"

//This code will be shortened after extraction code is done. 

bool phaseOneExtraction(string*& htmlTable) {

	string inputFileName = "input.txt";

// opening both input and output files
	ifstream inFS;
	inFS.open(inputFileName.c_str());

	if (!inFS.is_open()) {
		
		cout << "Error opening " << inputFileName << endl;

		return false;
	
	}

	// Search until you find the id "table1"
	
	//Check to see if id table1 was found at all. 
	cout << "Finding Table One" << endl;
	if (findTableOne(inFS)) {
		cout << "table1 found" << endl;
	}
	else {
		cout << "table1 not found." << endl;
		inFS.close();
		return false;
	}
	
	// Search until you find the <tbody> tag
	cout << "Finding tbody" << endl;
	if (findTBody(inFS)) {
		cout << "tbody found" << endl;
	}
	else {
		cout << "tbody not found.";
		inFS.close();
		return false;
	}

	//Phase 3: Extract all the text until </tbody> appears
	cout << "Beginning Extraction" << endl;
	if (textExtraction(inFS, htmlTable)) {
		cout << "Extraction complete" << endl;
	}
	else {
		cout << "An error occurred" << endl;
		return false;
	}

	cout << "End of phase one extraction." << endl;
	inFS.close();

	return true;
}

bool findTableOne(ifstream &inFS) {
	/* Helper function to extractCourseLine.*/
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
	/* Takes in a string one at a time and
	checks to see if it's '<'. If so, it 
	getlines until it reaches a '>'. If the 
	string is "tbody", it stops reading. If 
	not, then it continues. All inputs are 
	discarded.*/

	char tempChar;
	string tempString;

	// Get character with each iteration
	while (inFS >> tempChar) {
		
		// Check if char is '<'
		if (tempChar == '<') {
			//Get string until '>', exclude '>'
			getline(inFS, tempString, '>');

			//Check if tempString is "tbody"
			if (tempString == "tbody") {
				return true;
			}
		}

	}

	return false;
}

bool textExtraction(ifstream& inFS, string*& htmlTable) {
	/* Helper function to extractCourseLine
	Continues to extract until </tbody> is 
	reached. In addition, adds newlines to help
	with the readability of the html.
	*/
	char tempChar;
	string tempString;

	// Open output file
	/*ofstream outFS;
	outFS.open("filteredHTML.txt");

	if (!outFS.is_open()) {

		inFS.close();

		cout << "Error opening " << "filteredHTML.txt";
		return false;

	}*/

	// Begin 
	while (inFS >> tempChar) {

		if (tempChar == '<') {
			getline(inFS, tempString, '>');
			
			if (tempString == "/tbody") {
				//outFS.close();
				return true;
			}
			else if (tempString.find("tr ") != std::string::npos) {
				*htmlTable = *htmlTable + '<' + tempString + '>' + '\n';
				//outFS << '<' << tempString << '>' << endl;
			}
			else if (tempString == "/tr") {
				*htmlTable = *htmlTable + '<' + tempString + '>' + '\n' + '\n';
				//outFS << '<' << tempString << '>' << endl << endl;
			}
			else if (tempString == "/td") {
				*htmlTable = *htmlTable + '<' + tempString + '>' + '\n';
				//outFS << '<' << tempString << '>' << endl;
			}
			else {
				*htmlTable = *htmlTable + '<' + tempString + '>';
				//outFS << '<' << tempString << '>';
			}

		}
		else {
			*htmlTable = *htmlTable + tempChar;
			//outFS << tempChar;
		}

	}

//	outFS.close();

	return false;

}