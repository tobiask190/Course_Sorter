#pragma once

#include <string>;
#include <vector>

using namespace std;

class Course {

	public:
		Course();
		Course(vector<string*>&);

	private:
		int courseReferenceNumber;
		string subject;
		string subjectDescription;
		string courseNumber;
		int sequenceNumber;
		string courseTitle;
		int creditHours;
		string meetingTime;
		string instructor;
		string status;
		string reservedSeats;

};