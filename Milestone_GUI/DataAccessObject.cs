using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_GUI
{
    class DataAccessObject
    {
        // Instancne variable that points to the string that connects to our database 
        String connectionString = "datasource=localhost;port=3306;username=root;password=root;database=cst345_milestone";

        // Create empty generic list that will take a custom "Categories" class as the type parameter
        public List<Category> categories = new List<Category>();

        // Method for returning a list populated with all of our categories from the database
        public List<Category> getAllCategories() {

            List<Category> returnThese = new List<Category>();

            // Establish conneection with our MySQL database
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `product_categories`", connection);

            // Execute Reader method runs the SQL code from above into our database and returns a reader object
            // This reader object can then be accessed locally 
            using (MySqlDataReader reader = command.ExecuteReader()) { 

                while (reader.Read())
                {
                    Category category = new Category();

                    category.ID = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                    category.Description = reader.GetString(2);

                    returnThese.Add(category);
                }
            }

            connection.Close();
            return returnThese;
        }

        // Method for returning a list populated with all of our products from the database
        public List<Product> getAllProducts()
        {
            List<Product> returnThese = new List<Product>();

            // Establish conneection with our MySQL database
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT ID, name, description, price, product_categories_ID FROM `product`", connection);

            // Execute Reader method runs the SQL code from above into our database and returns a reader object
            // This reader object can then be accessed locally 
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Product product = new Product();

                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Description = reader.GetString(2);
                    product.Price = reader.GetDouble(3);
                    product.CategoryID = reader.GetInt32(4);

                    returnThese.Add(product);
                }
            }

            connection.Close();
            return returnThese;
        }
    
        public int deleteCategory(int categoryID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("DELETE FROM `product_categories` WHERE ID = @categoryID", connection);
            command.Parameters.AddWithValue("@categoryID", categoryID);

            int result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        public int deleteProduct(int productID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("DELETE FROM `product` WHERE ID = @categoryID", connection);
            command.Parameters.AddWithValue("@categoryID", productID);

            int result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }
    
        public List<Category> searchCategories(String inputString)
        {
            List<Category> returnThese = new List<Category>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Concatenate phrase will be the argument to the LIKE operation
            String searchPhrase = "%" + inputString + "%";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM `product_categories` WHERE name LIKE @search";
            command.Parameters.AddWithValue("@search", searchPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category();

                    category.ID = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                    category.Description = reader.GetString(2);

                    returnThese.Add(category);
                }
            }

            connection.Close();
            return returnThese;
        }

        public List<Product> searchProducts(String inputString)
        {
            List<Product> returnThese = new List<Product>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Concatenate phrase will be the argument to the LIKE operation
            String searchPhrase = "%" + inputString + "%";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM `product` WHERE name LIKE @search";
            command.Parameters.AddWithValue("@search", searchPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product();

                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Description = reader.GetString(2);
                    product.Price = reader.GetDouble(3);
                    product.CategoryID = reader.GetInt32(4);

                    returnThese.Add(product);
                }
            }

            connection.Close();
            return returnThese;
        }

        public List<Product> getProductsByCategoryID(int catID)
        {
            List<Product> returnThese = new List<Product>();

            // Establish conneection with our MySQL database
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT ID, name, description, price, product_categories_ID FROM `product` WHERE product_categories_ID = @catID", connection);
            command.Parameters.AddWithValue("@catID", catID);
            command.Connection = connection;

            // Execute Reader method runs the SQL code from above into our database and returns a reader object
            // This reader object can then be accessed locally 
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Product product = new Product();

                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Description = reader.GetString(2);
                    product.Price = reader.GetDouble(3);
                    product.CategoryID = reader.GetInt32(4);

                    returnThese.Add(product);
                }
            }

            connection.Close();
            return returnThese;
        }

        public int addOneCategory(Category category)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO `product_categories`(`name`, `description`) VALUES(@name, @description)", connection);

            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@description", category.Description);

            int numberOfRowsAdded = command.ExecuteNonQuery();
            connection.Close();

            return numberOfRowsAdded;
        }

        public int addOneProduct(Product product)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO `product`(`name`, `description`, `price`, `product_categories_ID`) VALUES (@name, @description, @price, @categoryID)", connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@categoryID", product.CategoryID);

            int numberOfRowsAdded = command.ExecuteNonQuery();
            connection.Close();

            return numberOfRowsAdded;
        }
    }
}
