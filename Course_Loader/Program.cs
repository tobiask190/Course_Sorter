using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Course_Loader {
    public enum SectionType {
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
    public struct Seats {
        int generalOpen;
        int generalMax;

        int generalWaitlistOpen;
        int generalWaitlistMax;

        int reservedOpen;
        int reservedMax;

        int reservedWaitlistOpen;
        int reservedWaitlistMax;
    }
    public class Course {
        public int crn;
        public string subject;
        public string courseNumber;
        public string sequenceNumber;
        public string title;
        public string units;
        public HashSet<DayOfWeek> days;

        public int startTime;
        public int endTime;

        public SectionType sectionType;
        public string building;
        public string room;
        public string startDate;
        public string endDate;
        public string instructor;

        public Seats seats;
    }
    class Program {
        static void Main(string[] args) {
            HashSet<string> errors = new HashSet<string>();

            HashSet<Course> courses = new HashSet<Course>();

            foreach (var f in Directory.GetFiles(@"C:\Users\alexm\source\repos\Course_Sorter\Course_Reader\bin\Debug\netcoreapp3.1", "*.html")) {
                HtmlDocument d = new HtmlDocument();
                d.LoadHtml(File.ReadAllText(f).Replace("&nbsp;", ""));
                var table = d.GetElementbyId("table1").SelectSingleNode("./tbody");
                //Each entry is a <tr> stored in the <tbody> with attributes specified in <td>
                foreach (var courseRow in table.ChildNodes) {
                    int crn = 0;
                    string subject = null;
                    string courseNumber = null;
                    string sequenceNumber = null;
                    string title = null;
                    string units = null;
                    HashSet<DayOfWeek> days = null;

                    int startTime = 0;
                    int endTime = 0;

                    SectionType type = 0;
                    string building = null;
                    string room = null;
                    string startDate = null;
                    string endDate = null;
                    string instructor = null;
                    Seats seats = new Seats();
                    foreach (var td in courseRow.SelectNodes("./td")) {
                        string text = td.InnerText;
                        switch (td.GetAttributeValue("data-property", null)) {
                            case "courseReferenceNumber":
                                crn = int.Parse(text);
                                break;
                            case "subject":
                                subject = text;
                                break;
                            case "courseNumber":
                                courseNumber = text;
                                break;
                            case "sequenceNumber":
                                sequenceNumber = text;
                                break;
                            case "courseTitle":
                                title = text;
                                break;
                            case "creditHours":
                                units = text;
                                break;
                            case "meetingTime":
                                var meeting = td.FirstChild;

                                //Some courses (ARC) have no meeting time
                                if (meeting == null) {
                                    continue;
                                }


                                text = meeting.FirstChild.FirstChild.InnerText;
                                if (text == "None") {
                                    days = new HashSet<DayOfWeek>();
                                } else {
                                    days = text.Split(",").Select(day => Enum.Parse<DayOfWeek>(day)).ToHashSet();
                                }

                                var timeStr = meeting.ChildNodes[1].InnerText.Split('-');
                                var (startStr, endStr) = (timeStr[0].Trim(), timeStr[1].Trim());

                                if (startStr.Any()) {
                                    var dt = DateTime.ParseExact(startStr, "hh:mm  tt", CultureInfo.InvariantCulture);
                                    startTime = (100 * dt.Hour) + dt.Minute;
                                }

                                if (endStr.Any()) {
                                    var dt = DateTime.ParseExact(endStr, "hh:mm  tt", CultureInfo.InvariantCulture);
                                    endTime = (100 * dt.Hour) + dt.Minute;
                                }

                                text = meeting.ChildNodes[2].InnerText;

                                type = Enum.Parse<SectionType>(text.Substring(text.IndexOf(':') + 1).Replace(" ", "_"));

                                text = meeting.ChildNodes[3].InnerText;
                                building = text.Substring(text.IndexOf(':') + 1);

                                text = meeting.ChildNodes[4].InnerText;
                                room = text.Substring(text.IndexOf(':') + 1);

                                startDate = meeting.ChildNodes[5].InnerText.Replace(" Start Date: ", null);
                                endDate = meeting.ChildNodes[6].InnerText.Replace(" End Date: ", null);
                                break;
                            case "instructor":
                                instructor = text;
                                break;
                            case "status":
                                //available = text;
                                break;
                            case "reservedSeats":
                                if(text.Any()) {
                                    Console.WriteLine(text);
                                }
                                break;
                            case null:
                                //End of <tr>
                                break;
                        }
                    }

                    courses.Add(new Course() {
                        crn = crn,
                        subject = subject,
                        courseNumber = courseNumber,
                        sequenceNumber = sequenceNumber,
                        title = title,
                        units = units,
                        days = days,
                        startTime = startTime,
                        endTime = endTime,
                        sectionType = type,
                        building = building,
                        room = room,
                        startDate = startDate,
                        endDate = endDate,
                        instructor = instructor,
                        seats = seats

                    });
                }

                /*
                foreach(var line in File.ReadAllLines(f)) {
                    if(line.Contains("table1")) {
                        var start = line.IndexOf("<table");
                        var s = "</table>";
                        var end = line.IndexOf(s) + s.Length;

                        s = line.Replace("&", "&amp;").Replace("<br>", "").Substring(start, end - start);
                        var doc = XDocument.Parse(s);
                        break;
                    }
                }
                //var doc = XDocument.Parse($"<Root>{File.ReadAllText(f)}</Root>");                
                */

                /*
                foreach(var n in GetNodes(doc.Root)) {
                    if(n.Attribute("id")?.Value == "table1") {
                        
                        break;
                    }
                }
                */
                /*
                IEnumerable<XElement> GetNodes(XElement root) {
                    var nodes = new Queue<XElement>();
                    nodes.Enqueue(root);
                    while(nodes.Any()) {
                        var e = nodes.Dequeue();
                        yield return e;
                        foreach(var c in e.Elements()) {
                            nodes.Enqueue(c);
                        }
                    }
                }
                */
            }
            return;
        }
    }
}
