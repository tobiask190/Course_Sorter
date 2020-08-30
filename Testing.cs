using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Terminal.Gui;

namespace Course_Sorter
{
    class Testing
    {
        public static void runTests()
        {
            //createMenu();
            //createWindow();
            createScroll();
        }
        static void createMenu()
        {
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
                Application.Top.Add(menu);
            }
        }
        static void createWindow()
        {
            var win = new Window("Testing")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var myView = new View();
            var secondView = new View();

            var label = new Label("Username: ")
            {
                X = 1,
                Y = 1,
                Width = 20,
                Height = 1
            };
            myView.Add(label);

            var username = new TextField("")
            {
                X = 1,
                Y = 2,
                Width = 30,
                Height = 1
            };
            myView.Add(username);

            var label1 = new Label("Greatings to All!")
            {
                X = 1,
                Y = 3,
                Width = 20,
                Height = 2
            };
            myView.Add(label);

            var label2 = new Label("Have a smacking day!")
            {
                X = 1,
                Y = 5,
                Width = 20,
                Height = 2
            };
            myView.Add(label);

            var label3 = new Label("Don't forget to floss!")
            {
                X = 1,
                Y = 7,
                Width = 20,
                Height = 2
            };
            myView.Add(label);


            var rect = new Rect(20, 4, 20, 4);
            var scrollview = new ScrollView(rect);
            var scrollbar = new ScrollBarView(rect, 4, 6, true );

            scrollview.Add(label1, label2, label3, scrollbar);

            //secondView.Add(scrollview);

            win.Add(scrollview);

            //win.Add(myView);

            Application.Top.Add(win);

            // Add both menu and win in a single call
        }
        public static void createScroll()
        {
            //Rect frame = new Rect(5, 5, 20, 2);

            //ScrollView scroll1 = new ScrollView(frame);
            //ScrollBarView bar1 = new ScrollBarView(frame, 5, 5, true);
            //var label = new Label(5, scroll1.ContentOffset.Y, "Hello");
            //var label2 = new Label(5, scroll1.ContentOffset.Y-1, "Hello2");
            //var label3 = new Label(5, scroll1.ContentOffset.Y-2, "Hello3");
            //var label4 = new Label(5, scroll1.ContentOffset.Y-3, "Hello4");
            //var label5 = new Label(5, scroll1.ContentOffset.Y-4, "Hello5");
            //var label6 = new Label(5, scroll1.ContentOffset.Y-5, "Hello6");
            //scroll1.ShowVerticalScrollIndicator = true;
            //scroll1.Add(label, label2, label3, label4, label5, label6);
            //Application.Top.Add(scroll1);


            var label1 = new Label("Hello 1");
            var label2 = new Label("Hello 2");
            var label3 = new Label("Hello 3");
            var label4 = new Label("Hello 4");
            var label5 = new Label("Hello 5");
            var label6 = new Label("Hello 6");
            var label7 = new Label("Hello 7");

            var scrollView = new ScrollView(new Rect(2, 2, 10, 5))
            {
                ContentSize = new Size(10, 10),
                ContentOffset = new Point(-1, -1),
                ShowVerticalScrollIndicator = true,
                ShowHorizontalScrollIndicator = false
            };

            scrollView.Add(label1, label2, label3, label4, label5, label6, label7);
            Application.Top.Add(scrollView);
            
        }
        static void updateScroll()
        {

        }
    }
}
