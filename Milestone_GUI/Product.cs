using System;

namespace Milestone_GUI
{
    // Class that represents a singular product from our database
    // Each field will be asssociated with an attribute from the table
    public class Product
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Price { get; set; }
        public int CategoryID { get; set; }
    }
}