using System;
using System.Collections.Generic;
using System.Text;

namespace Course_Sorter
{
    enum Days
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
    }
    enum SectionType
    {
        Lecture,
        Discussion,
        Lab,
        Studio,
        Other
    }

    class Course
    {
        public uint crn;
        public string subject;           //PHYS
        public string courseNumber;      //040C
        public string courseTitle;      //Want this in
        public string sequenceNumber;
        public uint units;
        public HashSet<Days> days;
        public uint startHours;
        public uint endHours;
        public SectionType sectionType;
        public string location;
        public HashSet<string> instructors;
        public uint totalSeats;     
        public uint generalSeats;       // Implement these later. 
        public uint reservedSeats;
    }
}
