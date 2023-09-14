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

namespace JoshuaRea_SchedulingApplication.Forms
{
    public partial class UpdateCustomer : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        //Constructors

        public UpdateCustomer()
        {
            InitializeComponent();
        }

        public UpdateCustomer(Customer customer, Address address, City city, Country country)
        {
            InitializeComponent();

            txtCustomerID.Text = customer.CustomerId.ToString();
            txtCustomerName.Text = customer.CustomerName;
            txtAddress.Text = address.Address1;
            txtAddress2.Text = address.Address2;
            txtCity.Text = city.CityName;
            txtCountry.Text = country.CountryName;
            txtPostalCode.Text = address.PostalCode;
            txtPhone.Text = address.Phone;
        }

        //Save Button

        private void btnSave_Click(object sender, EventArgs e)
        {
            Country country = new Country(txtCountry.Text);

            Country.GetOrCreateCountry(country);

            City city = new City(txtCity.Text, country.CountryId);

            City.GetOrCreateCity(city);

            Address address = new Address(txtAddress.Text, txtAddress2.Text, city.CityId, txtPostalCode.Text, txtPhone.Text);

            Address.GetOrCreateAddress(address);

            Customer customer = Customer.GetCustomer(Convert.ToInt32(txtCustomerID.Text));

            customer.CustomerName = txtCustomerName.Text;
            customer.AddressId = address.AddressId;
            customer.LastUpdate = DateTime.Now;
            customer.LastUpdatedBy = Login.currentUsername;

            Customer.UpdateCustomer(customer);

            main.RefreshCustomerGrid();
            this.Close();
        }

        //Cancel Button

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
