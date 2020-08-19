// The classes and their functions should have this general shape
// When implementing these classes, be sure to put them in separate files!!!
// Don't be afraid to delete the entire code in favor of better implementation

class courseCatalog {
	public List<Course> catalog = new List<Course>();
	// I believe this is not the best implementation... Please change it to a better one
	// Does the hash table go here? (Past me writing this when he hasn't fully researched hash tables)
}

class linkedCatalog {
	public List<Grouping> links = new List<Grouping>();
	
}

class Calendar {
	
	// Variables
	public List<Course> storedCourses = new List<Course>();
		// Stored courses are courses that have been dropped and dropped completely into the calendar
	public Course hoveringCourse = NULL; 
		// A possible course that is being hovered over the calendar but hasn't been dropped.
		// When drawing, these will have a lighter color than usual
	public List<#collision_placeholder_name#> collisions = new List <#collision_placeholder_name#>();
		// Used to take record which times have collisions. 
	
	// Functions
	public void addHovering ();
	public void discardHovering ();
		// These two functions add or delete the hovering course
		// They are called by another function
	public void timeCollisions ();
		// Searches the stored Courses list if there's a collision with the hovering course
		// Can't decide whether or not it returns a bool or just draws the course red directly.
	public void printCollision ();
		// If the user decides to add the colliding class anyways, it will draw a red box over the colliding times.
		
	// Helper Functions
	
}