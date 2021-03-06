#include "courseObject.h"
#include "phaseOneExtraction.h"
#include "phaseTwoExtraction.h"

Course::Course() {
	courseReferenceNumber = -1;
	subject = "null";
	subjectDescription = "null";
	courseNumber = "null";
	sequenceNumber = -1;
	courseTitle = "null";
	creditHours = -1;

	startTime = -1;
	endTime = -1;
	classType = "";
	building = "";
	room = "";
	startDate = "";
	endDate = "";
	
	seatsTotal = -1;
	seatsLeft = -1;
	waitTotal = -1;
	waitLeft = -1;
	generalTotal = -1;
	generalLeft = -1;
	reservedLeft = -1;
	reservedTotal = -1;
}

Course::Course(const vector<string*>& course) {
	courseReferenceNumber = stoi(*course.at(0));
	subject = *course.at(1);
	subjectDescription = *course.at(2);
	courseNumber = *course.at(3);
	sequenceNumber = stoi(*course.at(4));
	courseTitleSubconstructor(*course.at(5));
	creditHours = stoi(*course.at(6));
	meetingTimeSubconstructor(*course.at(7));
	instructorSubconstructor(*course.at(8));
	classStatusSubconstructor(*course.at(9));
	reservedStatusSubconstructor(*course.at(10));
}

void Course::printAll() {
	
	cout << "CRN: " << courseReferenceNumber << endl;
	cout << "Subject: " << subject << endl;
	cout << "Subject Description: " << subjectDescription << endl;
	cout << "Course Number: " << courseNumber << endl;
	cout << "Sequence Number: " << sequenceNumber << endl;
	cout << "Course Title: " << courseTitle << endl;
	cout << "Credit Hours: " << creditHours << endl;

	cout << "Weekdays: ";
	for (unsigned i = 0; i < weekdays.size(); i++) {
		cout << weekdays.at(i) << ' ';
	}
	cout << endl;

	cout << "Start Hour: " << startTime << endl;
	cout << "End Hour: " << endTime << endl;
	cout << "Class Type: " << classType << endl;
	cout << "Building: " << building << endl;
	cout << "Room: " << room << endl;
	cout << "Start Date: " << startDate << endl;
	cout << "End Date: " << endDate << endl;

	cout << "Instructors: " << endl;
	for (unsigned i = 0; i < instructors.size(); i++) {
		cout << instructors.at(i) << ' ' << instructorEmails.at(i) << endl;
	}
	cout << "Seats: " << seatsLeft << " / " << seatsTotal << endl;
	cout << "Waitlist: " << waitLeft << " / " << waitTotal << endl;

	cout << "General: " << generalLeft << " / " << generalTotal << endl;
	cout << "Reserved: " << reservedLeft << " / " << reservedTotal << endl;

}

// Sub Constructors and constructor helpers
void Course::courseTitleSubconstructor(const string& titleString) {
	unsigned endIndex = -1;

	for (unsigned i = 0; i < titleString.size(); i++) {
		if (islower(titleString.at(i)) != false) {
			endIndex = i - 1;
			break;
		}
	}

	courseTitle = titleString.substr(0, endIndex);
}

void Course::meetingTimeSubconstructor(const string& meetingString) {

	unsigned weekdaysIndex;
	unsigned typeIndex;
	unsigned buildingIndex;
	unsigned roomIndex;
	unsigned startDateIndex;
	unsigned endDateIndex;

	weekdaysIndex = meetingString.find("SMTWTFS");
	typeIndex = meetingString.find("Type:");
	buildingIndex = meetingString.find("Building:");
	roomIndex = meetingString.find("Room:");
	startDateIndex = meetingString.find("StartDate:");
	endDateIndex = meetingString.find("EndDate:");

	// Set the booleans for the weekdays
	weekdays.resize(7);
	
	for (unsigned i = 0; i < weekdays.size(); i++) {
		weekdays.at(i) = false;
	}

	for (unsigned i = 0; i < weekdaysIndex; i++) {
		weekdaySwitch(meetingString, i);
	}

	// Set the hours for the course
	hourSubconstructor(meetingString, weekdaysIndex + 7);

	// Set the class type
	classType = meetingString.substr(typeIndex + 11, buildingIndex - typeIndex - 11);

	// Set the building
	building = meetingString.substr(buildingIndex + 9, roomIndex - buildingIndex - 9);

	// Set the room
	room = meetingString.substr(roomIndex + 5, startDateIndex - roomIndex - 5);
	
	// Set start date
	startDate = meetingString.substr(startDateIndex + 10, endDateIndex - startDateIndex - 10);

	// Set end date
	endDate = meetingString.substr(endDateIndex + 8);

}

void Course::instructorSubconstructor(const string& instructorString) {

	unsigned pointedIndex = 0;
	unsigned indexOne = 0;
	unsigned instructorIndex = 0;

	for (unsigned i = 0; i < instructorString.size(); i++) {
	
		if (instructorString.at(i) != '|') {
		
			if (instructorString.find('#', i) != std::string::npos) {

				if (instructorString.find("TheStaff") != std::string::npos) {
					instructors.push_back("TheStaff");
				}
			
				else {
					instructors.push_back("Not Specified");
				}
				
				instructorEmails.push_back("");
				i = instructorString.find('|', i) - 1;
				instructorIndex++;
			}

			else {

				indexOne = instructorString.find('/', i);

				instructorEmails.push_back(instructorString.substr(i, indexOne - i).erase(0,7));

				instructors.push_back(instructorString.substr(
					instructorString.find('/', i) + 1,
					instructorString.find('|', i) - instructorString.find('/', i) - 1)
				);
				i = instructorString.find('|', i) - 1;
				instructorIndex++;
			}
		}

		else if (instructorString.at(i + 1) == '(') {
			instructors.at(instructorIndex - 1).append(" (Primary)");
			i = instructorString.find('|', i + 1) - 1;
		}

		else if (instructorString.at(i + 1) == '|') {
		
			if (instructors.empty() == true) {
				instructors.push_back("Not Assigned");
				instructorEmails.push_back("");
			}
			break;
		}
	}
}

void Course::classStatusSubconstructor(const string& classStatusString) {	
	
	vector<string> stringTemps;

	for (unsigned i = 0; i < classStatusString.size(); i++) {
		if (isdigit(classStatusString.at(i))) {
			stringTemps.push_back(classStatusString.substr(i));
			while (isdigit(classStatusString.at(i))) {
				i++;
			}
		}
	}

	seatsLeft = stoi(stringTemps.at(0));
	seatsTotal = stoi(stringTemps.at(1));
	if (stringTemps.size() > 2) {
		waitLeft = stoi(stringTemps.at(2));
		waitTotal = stoi(stringTemps.at(3));
	}
	else {
		waitLeft = 0;
		waitTotal = 0;
	}

}

void Course::reservedStatusSubconstructor(const string& resservedString) {
	vector<string> stringTemps;

	for (unsigned i = 0; i < resservedString.size(); i++) {
		if (isdigit(resservedString.at(i))) {
			stringTemps.push_back(resservedString.substr(i));
			while (isdigit(resservedString.at(i))) {
				i++;
			}
		}
	}

	if (stringTemps.size() > 2) {
		generalLeft = stoi(stringTemps.at(0));
		generalTotal = stoi(stringTemps.at(1));
		reservedLeft = stoi(stringTemps.at(2));
		reservedTotal = stoi(stringTemps.at(3));
	}
	else {
		generalLeft = seatsLeft;
		generalTotal = seatsTotal;
		reservedLeft = 0;
		reservedTotal = 0;
	}
}

void Course::weekdaySwitch(const string& meetingString, unsigned index) {
	switch (meetingString.at(index)) {
	case 'S':
		if (meetingString.at(index + 1) == 'u') {
			weekdays.at(0) = true;
		}
		else {
			weekdays.at(6) = true;
		}
		break;
	case 'M':
		weekdays.at(1) = true;
		break;
	case 'T':
		if (meetingString.at(index + 1) == 'u') {
			weekdays.at(2) = true;
		}
		else {
			weekdays.at(4) = true;
		}
		break;
	case 'W':
		weekdays.at(3) = true;
		break;
	case 'F':
		weekdays.at(5) = true;
		break;
	default:
		break;
	}

}

void Course::hourSubconstructor(const string& meetingString, unsigned startIndex) {

	string startString;
	string endString;

	unsigned dashIndex = meetingString.find('-');
	
	startString = meetingString.substr(startIndex, 7);
	endString = meetingString.substr(dashIndex + 1, 7);

	startString.erase(2, 1);
	endString.erase(2, 1);

	if (startString.find("AM")) {
		startTime = stoi(startString);
	}
	else {
		startTime = stoi(startString) + 1200;
	}

	if (endString.find("AM")) {
		endTime = stoi(endString);
	}
	else {
		endTime = stoi(endString) + 1200;
	}
	
}


bool fullCourseExtraction(vector<Course*>& courseList) {
	
	string* htmlTable = new string;

	if (!phaseOneExtraction(htmlTable)) {
		cout << "Something went wrong in phase one." << endl;
		return false;
	}

	if (!phaseTwoExtraction(htmlTable, courseList)) {
		cout << "Something went wrong in phase two" << endl;
		return false;
	}

	return 1;
}