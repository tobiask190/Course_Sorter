#include "courseObject.h"

Course::Course() {
	courseReferenceNumber = -1;
	subject = "null";
	subjectDescription = "null";
	courseNumber = "null";
	sequenceNumber = -1;
	courseTitle = "null";
	creditHours = -1;
	meetingTime = "null";
	instructor = "null";
	status = "null";
	reservedSeats = "null";
}

Course::Course(vector<string*>& course) {
	courseReferenceNumber = stoi(*course.at(0));
	subject = *course.at(1);
	subjectDescription = *course.at(2);
	courseNumber = *course.at(3);
	sequenceNumber = stoi(*course.at(4));
	courseTitle = *course.at(5);
	creditHours = stoi(*course.at(6));
	meetingTime = *course.at(7);
	instructor = *course.at(8);
	status = *course.at(9);
	reservedSeats = *course.at(10);
}