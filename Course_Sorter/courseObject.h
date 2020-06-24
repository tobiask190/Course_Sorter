#pragma once

#include <string>
#include <vector>
#include <iostream>
#include <cctype>

using namespace std;

class Course {

	public:
		Course();
		Course(const vector<string*>&);	//Special case that constructs using custom filtered information.
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
		
		vector<bool> weekdays;
		////Note that it will be a 24 hour clock
		////Also it will be 0000 - 2400
		int startTime;
		int endTime;
		//
		string classType; 
		string building;
		string room;
		//
		////These will be kept as strings.
		string startDate;
		string endDate;

		string instructor;
		vector<string> instructors;
		vector<string> instructorEmails;
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

		private: //Helper Constructors
			void courseTitleSubconstructor(const string&);
			void meetingTimeSubconstructor(const string&);
			void instructorSubconstructor(const string&);
			void classStatusSubconstructor(const string&);
			void reservedStatusSubconstructor(const string&);

			void weekdaySwitch(const string&, unsigned);
			void hourSubconstructor(const string&, unsigned);
			
			
};