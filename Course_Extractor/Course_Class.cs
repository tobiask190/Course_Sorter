using System;
using System.Collections.Generic;
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
        public string time;

        public SectionType type;
        public string building;
        public string room;
        public string startDate;
        public string endDate;
        public string instructor;
        public string available;
        public string reserved;
    }
}
