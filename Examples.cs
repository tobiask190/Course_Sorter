using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Terminal.Gui;

namespace Course_Sorter
{
    public class Example
    {
        // Uses the code from https://migueldeicaza.github.io/gui.cs/articles/overview.html
        // Go through each code in conjunction with the text
        // Don't uncomment them all at once.

        public static void runExamples()
        {
            //createQueries();

            //createLabels();

            createMenu();

            // Creates a writable textbox
            /*var newview = new View();
            SetupMyView(newview);
            Application.Top.Add(newview);*/

            //layoutDemo();
        }

        static int createQueries()
        {
        // Creates a text box that can be interacted with via buttons. Returns a value
            var n = MessageBox.Query(50, 7,
            "Question", "Do you like console apps?", "Yes", "No"
            );
            return n;
        }

        static void createLabels()
        {
         // Creates a textbox that cannot be interacted with
            var label = new Label("Hello World")
            {
                X = Pos.Center(),    
                Y = Pos.Center(),   
                Height = 3             
            };
 
            Application.Top.Add(label);
            
        }

        static void createMenu()
        // Creates a dropdown menu with the option to quit
        {
            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => {
                    Application.RequestStop ();
                })
            }),
        });

            var win = new Window("Hello")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            // Add both menu and win in a single call
            Application.Top.Add(menu, win);
        }

        static void SetupMyView(View myView)
        // Creates a textbox that the user can write in
        {
            // Adds a label to the object
            var label = new Label("Username: ")
            {
                X = 1,
                Y = 1,
                Width = 20,
                Height = 1
            };
            myView.Add(label);

            // Adds a line where users can type
            var username = new TextField("")
            {
                X = 1,
                Y = 2,
                Width = 30,
                Height = 1
            };
            myView.Add(username);
        }

        static void layoutDemo()
        {
            // Dynamically computed.
            var label = new Label("Hello")
            {
                X = 1,
                Y = Pos.Center(),
                Width = Dim.Fill(),
                Height = 1
            };

            // Absolute position using the provided rectangle
            var label2 = new Label(new Rect(1, 2, 20, 1), "World");
                // X = 1, Y = 2, Width = 20, Height = 1

            Application.Top.Add(label, label2);
        }



    }





}