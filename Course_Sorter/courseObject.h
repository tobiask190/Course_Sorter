#pragma once

#include <string>;
#include <vector>
#include <iostream>

using namespace std;

class Course {

	public:
		Course();
		Course(vector<string*>&);
		void printAll();

	private:
		int courseReferenceNumber;
		string subject;
		string subjectDescription;
		string courseNumber;
		int sequenceNumber;
		string courseTitle;
		int creditHours;

		string meetingTime;
		/*
		vector<string> weekdays;
		//Note that it will be a 24 hour clock
		//Also it will be 0000 - 2400
		int startTime;
		int endTime;
		
		string type; 
		string building;
		string room;
		
		//These will be kept as strings.
		string startDate;
		string endDate;
		*/

		string instructor;
		//vector<string> instructors
		//vector<string> instructorEmails;
		//Add in email
		
		string status;
		string reservedSeats;
		/*
		int seatsTotal;
		int seatsLeft;
		int generalTotal;
		int generalLeft;
		int reservedTotal;
		int reservedLeft;
		*/

};