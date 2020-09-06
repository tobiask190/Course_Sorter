using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using HtmlAgilityPack;

namespace Course_Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            extractionWrapper.extractionExecutable();
        }
    }

    class extractionWrapper
    {

        public static void extractionExecutable()
        {
            setCoursePagesDir();
            var pageListing = fileNameLister();
            //var CourseList = courseExtraction(pageListing);
            fileExtraction(pageListing[0]);
        }

        static void setCoursePagesDir()
        {
            // Sets the directory to where the pages are stored.
            for (int i = 0; i < 3; i++)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var parentDirectory = Directory.GetParent(currentDirectory);
                Directory.SetCurrentDirectory(parentDirectory.ToString());
            }
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Course_Pages");
        }
        static List<string> fileNameLister()
        {
            // Does a full extraction of all file names inside of the directory (removes everything except file name)
            string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory());
            int lastSlashIndex = -1;

            for (int i = 0; i < fileList[0].Length; i++)
            {
                if (fileList[0][i] == '\\')
                {
                    lastSlashIndex = i;
                }
            }

            for (uint i = 0; i < fileList.Length; i++)
            {
                fileList[i] = fileList[i].Remove(0, lastSlashIndex + 1);
            }
            
            return fileList.ToList();
        }

        //static List<Course> courseExtraction(List<string> pageList)
        //{
        //    // Takes in page list and does intializes for each page a file extraction that returns a list of courses to be appended
        //    // onto the main list of course objects. The main list will be returned
        //    List<Course> courseList = new List<Course>();
        //    for (int i = 0; i < pageList.Count(); i++)
        //    {
        //        courseList.AddRange(fileExtraction(pageList[i]));
        //    }
        //    return courseList;
        //}
        
        static void fileExtraction(string file) // Change back to List<Course> after finishing
        {
            // Course List
            List<Course> courseList = new List<Course>();

            // Open the file and load contents
            string fileContents = File.ReadAllText(file);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(fileContents);

            // Get only the html table document
            var table = htmlDoc.GetElementbyId("table1");

            int i = 0;

            foreach (var row in table.SelectNodes(".//tr").Skip(1))
            {
                List<string> courseElements = new List<string>();
                Console.Write("Course: " + i + '\n');
                i++;

                int j = 0;
                foreach (var cell in row.ChildNodes)
                {
                    //Adds in the element(string) here and doesn't do anything else. Constructor will do heavy lifting
                    if (j < 11)
                    {
                        courseElements.Add(cell.InnerText);
                    }
                    j++;
                }

                // Create course and append it to the course list. 
                Course test = new Course(courseElements);
                Console.WriteLine("Hello");
                test.printElements();

                //foreach (var element in courseElements)
                //{
                //    Console.WriteLine("Element: " + element);
                //}

                Console.Write('\n');

            }
        }

            public static void processHTML(string fileName)
        {

        }
    }

}
