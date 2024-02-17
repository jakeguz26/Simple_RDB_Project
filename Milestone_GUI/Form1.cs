using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milestone_GUI
{
    public partial class Form1 : Form
    {

        BindingSource categoryBindingSource = new BindingSource();
        BindingSource productBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        // This is the button for showiing all the product categories
        private void button2_Click(object sender, EventArgs e)
        {
            DataAccessObject dataAccessObject = new DataAccessObject();
            categoryBindingSource.DataSource = dataAccessObject.getAllCategories();
            dataGridView.DataSource = categoryBindingSource;
        }

        // This is the button for showing all the individual products
        private void button3_Click(object sender, EventArgs e)
        {
            DataAccessObject dataAccessObject = new DataAccessObject();
            productBindingSource.DataSource = dataAccessObject.getAllProducts();
            dataGridView2.DataSource = productBindingSource;
        }

        // This is the button that DELETES category records
        private void button4_Click(object sender, EventArgs e)
        {
            // Returns the index of the record from our table
            int rowClicked = dataGridView.CurrentRow.Index;
       
            // Returns the integer that represents the ID of our selected Categroy record
            int categoryID = (int)dataGridView.Rows[rowClicked].Cells[0].Value;
            String categoryName = (String)dataGridView.Rows[rowClicked].Cells[1].Value;

            MessageBox.Show("You selected the " + categoryName + " category.");

            DataAccessObject dataAccessObject = new DataAccessObject();

            // Calls method that performs the delete operation and returns the # of deleted rows
            int result = dataAccessObject.deleteCategory(categoryID);

            MessageBox.Show(result + " category deleted.");

            dataGridView.DataSource = null;
            List<Category> categories = dataAccessObject.getAllCategories();
        }

        // This is the button that DELETES product records
        private void button1_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView2.CurrentRow.Index;

            int productID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;
            String productName = (String)dataGridView2.Rows[rowClicked].Cells[1].Value;

            MessageBox.Show("You selected the " + productName + " product.");

            DataAccessObject dataAccessObject = new DataAccessObject();

            int result = dataAccessObject.deleteProduct(productID);
            MessageBox.Show(result + " deleted.");

            dataGridView2.DataSource = null;
            List<Product> products = dataAccessObject.getAllProducts();
        }

        // This button is to search Category names
        // Input source will be textBox3
        private void button5_Click(object sender, EventArgs e)
        {
            DataAccessObject dataAccessObject = new DataAccessObject();
            categoryBindingSource.DataSource = dataAccessObject.searchCategories(textBox3.Text);
            dataGridView.DataSource = categoryBindingSource;
        }

        // This button is to search Product names
        // Input will be textBox4
        private void button6_Click(object sender, EventArgs e)
        {
            DataAccessObject dataAcccessObject = new DataAccessObject();
            productBindingSource.DataSource = dataAcccessObject.searchProducts(textBox4.Text);
            dataGridView2.DataSource = productBindingSource;
        }

        // This action is assocaited with clicking the cell of CATEGORY TABLE
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            int rowClicked = dataGridView.CurrentRow.Index;
            int categoryID = (int)dataGridView.Rows[rowClicked].Cells[0].Value;

            MessageBox.Show("You clicked " + rowClicked + " which contains category " + categoryID);

            DataAccessObject dataAccessObject = new DataAccessObject();

            // Return a list of all the releveant products by passing in the ID of the cell clicked
            productBindingSource.DataSource = dataAccessObject.getProductsByCategoryID(categoryID);
            dataGridView2.DataSource = productBindingSource;
        }

        // This button will add a new category record to our database
        private void button7_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            DataAccessObject dataAccessObject = new DataAccessObject();

            category.Name = textBox5.Text;
            category.Description = textBox6.Text;

            int result = dataAccessObject.addOneCategory(category);
            MessageBox.Show(result + " new row(s) inserted.");
        }

        // This button will add a new product record to our database
        private void button8_Click(object sender, EventArgs e)
        {
            // textBox 7 through 10 will be used to collect data
            Product product = new Product();
            DataAccessObject dataAccessObject = new DataAccessObject();

            product.Name = textBox7.Text;
            product.Description = textBox8.Text;
            product.Price = Double.Parse(textBox9.Text);
            product.CategoryID = Int32.Parse(textBox10.Text);

            int result = dataAccessObject.addOneProduct(product);
            MessageBox.Show(result + " new row(s) inserted.");   
        }






        // ------------ IGNORE THIS METHOD ----------------------
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // ------------ IGNORE THIS METHOD ------------------
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ------------ IGNORE THIS METHOD --------------------
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
