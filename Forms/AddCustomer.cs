using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoshuaRea_SchedulingApplication.Classes;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public partial class AddCustomer : Form
    {

        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        //Constructor

        public AddCustomer()
        {
            InitializeComponent();
            txtCustomerID.Text = Customer.getNewCustomerID().ToString();
            BtnSaveDisable();
            
        }

        //Method to check if Save button should be disabled

        private void BtnSaveDisable()
        {
            if (
                string.IsNullOrWhiteSpace(txtCustomerName.Text) || 
                string.IsNullOrWhiteSpace(txtAddress.Text) || 
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtCountry.Text) ||
                string.IsNullOrWhiteSpace(txtPostalCode.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text)
                )
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        //Method for when Save button is clicked

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool customerExists = false;
            string existsString = "SELECT customerName FROM customer WHERE customer.customerName = " + $"'{txtCustomerName.Text}'";
            MySqlCommand cmd = new MySqlCommand(existsString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr.HasRows)
            {
                customerExists = true;

            }
            rdr.Close();
            if (customerExists == false)
            {

                //Create a new Country Class using Country Textbox
                Country country = new Country(txtCountry.Text);

                //Create a new Country in the Database
                Country.GetOrCreateCountry(country);

                //Create a new City class
                City city = new City(txtCity.Text, country.CountryId);

                //Create a new city in the database
                City.GetOrCreateCity(city);

                //Create a new Address class
                Address newAddress = new Address(txtAddress.Text, txtAddress2.Text, city.CityId, txtPostalCode.Text, txtPhone.Text);

                //Create a new Address in the database
                Address.GetOrCreateAddress(newAddress);


                //Create a new customer class
                Customer newCustomer = new Customer(Convert.ToInt32(txtCustomerID.Text), txtCustomerName.Text, newAddress.AddressId, 1);

                //Create Customer in Database
                Customer.CreateCustomer(newCustomer);
                
                //Refresh Grid and Close Form
                main.RefreshCustomerGrid();
                this.Close();
            }
            else
            {
                MessageBox.Show("Customer already exists. Please update customer.");
            }
            
        }

        //Check Change on Textboxes

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }

        private void txtPostalCode_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            BtnSaveDisable();
        }
    }
}
