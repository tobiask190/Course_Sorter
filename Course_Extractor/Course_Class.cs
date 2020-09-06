using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Course_Extractor
{
    enum Hours
    {
        _0800, _0830,
        _0900, _0930,
        _1000, _1030,
        _1100, _1130,
        _1200, _1230,
        _1300, _1330,
        _1400, _1430,
        _1500, _1530,
        _1600, _1630,
        _1700, _1730,
        _1800, _1830,
        _1900, _1930,
        _2000, _2030,
    }
    enum SectionType
    {
        Unknown,
        Lecture,
        Discussion,
        Laboratory,
        Studio,
        Workshop,
        Seminar,
        Individualized_Study,
        Research,
        Field,
        Consultation,
        Class,
        Written_Work,
        Practicum,
        Internship,
        Colloquium,
        Clinic,
        Screening,
        Tutorial,
        Thesis,
        Activity,
        Term_Paper,
        Additional_Lecture,
        XXX
    }
    class Course
    {
        public int crn;
        public string subject;
        public string courseNumber;
        public string sequenceNumber;
        public string title;
        public string units;
        public HashSet<DayOfWeek> days;

        //Convert this to Hours later?
        public int startTime;
        public int endTime;

        public SectionType type;
        public string building;
        public string room;
        public string startDate;
        public string endDate;
        public HashSet<string> instructors;
        public string available;
        public string reserved;

        public Course()
        {
            crn = -1;
            subject = "Unknown";
            courseNumber = "???";
            sequenceNumber = "???";
            title = "???";
            units = "???";
            days = new HashSet<DayOfWeek>();

            //Convert this to Hours later?
            startTime = -1;
            endTime = -1;

            type = SectionType.Unknown;
            building = "???";
            room = "???";
            startDate = "???";
            endDate = "???";
            available = "???";
            reserved = "???";
        }
        public Course(List<string> elementList)
        {
            Console.WriteLine("Place 1");
            Console.WriteLine(elementList[0]);
            crn = int.Parse(elementList[0]);
            Console.WriteLine("Place 2");
            subject = elementList[1];
            Console.WriteLine("Place 3");
            courseNumber = elementList[3];
            Console.WriteLine("Place 4");
            sequenceNumber = elementList[4];
            Console.WriteLine("Place 5");
            courseTitleSubconstructor(elementList[5]);
            Console.WriteLine("Place 6");
            units = elementList[6];
            Console.WriteLine("Place 7");
            meetingTimeSubconstructor(elementList[7]);
            Console.WriteLine("Place 8");
            instructorSubconstructor(elementList[8]);
            Console.WriteLine("Place 9");
            available = elementList[9];
            Console.WriteLine("Place 10");
            reserved = elementList[10];
            Console.WriteLine("Place 11");
        }

        private void courseTitleSubconstructor(string input)
        {
            if (input.Length == 0)
            {
                return;
            }
            int endIndex = -1;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLower(input[i]) == true)
                {
                    endIndex = i - 1;
                    break;
                }
            }
            title = input.Substring(0, endIndex);
        }
        private void meetingTimeSubconstructor(string input)
        {
            if (input.Length == 0)
            {
                return;
            }

            var typeIndex       = input.IndexOf("Type:");
            var buildingIndex   = input.IndexOf("Building:");
            var roomIndex       = input.IndexOf("Room:");
            var startDateIndex  = input.IndexOf("Start Date:");
            var endDateIndex    = input.IndexOf("End Date:");

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (input.Contains(day.ToString()))
                {
                    days.Add(day);
                }
            }

            foreach (SectionType type in Enum.GetValues(typeof(SectionType)))
            {
                if (input.Contains(type.ToString())) {
                    this.type = type;
                    break;
                }
                else if (type == SectionType.XXX)
                {
                    this.type = SectionType.XXX;
                }
            }

            room = input.Substring(roomIndex + 6, input.IndexOf('0', roomIndex + 6) - (roomIndex + 6));
            startDate = input.Substring(startDateIndex + 12, input.IndexOf('0', startDateIndex + 12) - (startDateIndex + 12));
            endDate = input.Substring(endDateIndex + 10, input.IndexOf('0', endDateIndex + 10) - (endDateIndex + 10));


        }
        private void instructorSubconstructor(string input)
        {

            int currentIndex = 0;

            if (input.Length == 0)
            {
                return;
            }

            while(true)
            {
                var endSubString = input.IndexOf("  ", currentIndex);
                if (endSubString == -1)
                {
                    break;
                }
                instructors.Add(input.Substring(currentIndex, endSubString - currentIndex));
                currentIndex = endSubString + 2;
            }
        }
        public void printElements()
        {
            Console.WriteLine(crn);
            Console.WriteLine(subject);
            Console.WriteLine(courseNumber);
            Console.WriteLine(sequenceNumber);
            Console.WriteLine(title);
            Console.WriteLine(units);
            foreach (var day in days)
            {
                Console.Write(day + ' ');
            }
            Console.Write('\n');
            //Console.WriteLine(startTime);
            //Console.WriteLine(endTime);
            Console.WriteLine(type);
            Console.WriteLine(building);
            Console.WriteLine(room);
            Console.WriteLine(startDate);
            Console.WriteLine(endDate);
            foreach (var instructor in instructors)
            {
                Console.Write(instructor + ' ');
            }
            Console.Write('\n');
            Console.WriteLine(available);
            Console.WriteLine(reserved);

        }
    }
}
