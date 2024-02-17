using System;

namespace Milestone_GUI
{
    // Class that represents a singular category from our database
    // Each field will be asssociated with an attribute from the table
    public class Category
    {   
        // ID field will be associated with ID (PK) attribute from table
        public int ID { get; set; }

        // Name field will be associated with Name attribute from table
        public String Name { get; set; }

        // Description field will be associated with Desccription atttribute from table
        public String Description { get; set; }
    }
}