﻿using System;
using System.ComponentModel.DataAnnotations;
using Terminal.Gui;

namespace Course_Sorter
{
    class Program
    {


        static int Main(string[] args)
        {
            // Setup course object information before the Init.

            Application.Init(); 

            Example.runExamples();

            Application.Run();
            return 0;
        }

    }

}
