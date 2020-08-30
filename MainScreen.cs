using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Course_Sorter {
    class MainScreen {
		Window Main;
		public MainScreen() {
			Main = new Window(new Rect(0, 0, 80, 40), "Banner");
			
			var CourseListing = new ScrollView(new Rect(1, 1, 20, 20), new Rect(0, 0, 20, 100)) {
				ShowHorizontalScrollIndicator = true,
				ShowVerticalScrollIndicator = true,
			};
			CourseListing.Scrolled += s => {
				int a = 0;
			};
			
			for(int y = 0; y < 90; y++) {
				CourseListing.Add(new Label("test" + y) { X = 0, Y = y });
            }
			
			Main.Add(CourseListing);
		}
		public void Start() {
			Application.Top.Add(Main);
        }
	}
}
