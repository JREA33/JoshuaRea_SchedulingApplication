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
using JoshuaRea_SchedulingApplication.Forms;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public partial class Main : Form
    {
        public static DataTable customerData = new DataTable();
        public static DataTable appointmentData = new DataTable();
        public Main()
        {
            InitializeComponent();

            //Setup DataGridView Sources
            dgvCustomers.DataSource = getCustomers();
            dgvAppointments.DataSource = getAllAppointments();

            //Format dgvCustomers
            dgvCustomers.Columns[0].HeaderText = "Customer ID";
            dgvCustomers.Columns[0].Width = 125;
            dgvCustomers.Columns[1].HeaderText = "Name";
            dgvCustomers.Columns[1].Width = 125;
            dgvCustomers.Columns[2].HeaderText = "Address";
            dgvCustomers.Columns[2].Width = 125;
            dgvCustomers.Columns[3].HeaderText = "City";
            dgvCustomers.Columns[3].Width = 125;
            dgvCustomers.Columns[4].HeaderText = "Country";
            dgvCustomers.Columns[4].Width = 125;
            dgvCustomers.Columns[5].HeaderText = "Phone";
            dgvCustomers.Columns[5].Width = 125;
        }


        //Method to Populate dgvCustomers

        public static DataTable getCustomers()
        {
            string sqlString = 
                "SELECT customerId, customerName, address, city, country, phone " +
                "FROM customer " +
                "INNER JOIN address " +
                "ON customer.addressId = address.addressId " +
                "INNER JOIN city " +
                "ON address.cityId = city.cityId " +
                "INNER JOIN country " +
                "ON city.countryId = country.countryId";
            MySqlCommand cmd = new MySqlCommand(sqlString, DBConnection.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(customerData);
            return customerData;
        }

        //Methods to Populate dgvAppointments

        public static DataTable getAllAppointments()
        {
            string sqlString = "SELECT * FROM appointment;";
            MySqlCommand cmd = new MySqlCommand(sqlString, DBConnection.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(appointmentData);

            for (int i = 0; i < appointmentData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentData.Rows[i]["start"];
                appointmentData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentData.Rows[i]["end"];
                appointmentData.Rows[i]["end"] = end.ToLocalTime();

                DateTime createDate = (DateTime)appointmentData.Rows[i]["createDate"];
                appointmentData.Rows[i]["createDate"] = createDate.ToLocalTime();

                DateTime lastUpdate = (DateTime)appointmentData.Rows[i]["lastUpdate"];
                appointmentData.Rows[i]["lastUpdate"] = lastUpdate.ToLocalTime();
            }
            return appointmentData;
        }

        public static DataTable getMonthsAppointments()
        {
            string monthAdded = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlString = $"SELECT * FROM appointment WHERE start >= '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' and start <= '{monthAdded}';";

            MySqlCommand cmd = new MySqlCommand(sqlString, DBConnection.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(appointmentData);

            for (int i = 0; i < appointmentData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentData.Rows[i]["start"];
                appointmentData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentData.Rows[i]["end"];
                appointmentData.Rows[i]["end"] = end.ToLocalTime();

                DateTime createDate = (DateTime)appointmentData.Rows[i]["createDate"];
                appointmentData.Rows[i]["createDate"] = createDate.ToLocalTime();

                DateTime lastUpdate = (DateTime)appointmentData.Rows[i]["lastUpdate"];
                appointmentData.Rows[i]["lastUpdate"] = lastUpdate.ToLocalTime();
            }

            return appointmentData;
        }

        public static DataTable getWeeksAppointments()
        {
            string weekAdded = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlString = $"SELECT * FROM appointment WHERE start >= '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' and start <= '{weekAdded}';";

            MySqlCommand cmd = new MySqlCommand(sqlString, DBConnection.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(appointmentData);

            for (int i = 0; i < appointmentData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentData.Rows[i]["start"];
                appointmentData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentData.Rows[i]["end"];
                appointmentData.Rows[i]["end"] = end.ToLocalTime();

                DateTime createDate = (DateTime)appointmentData.Rows[i]["createDate"];
                appointmentData.Rows[i]["createDate"] = createDate.ToLocalTime();

                DateTime lastUpdate = (DateTime)appointmentData.Rows[i]["lastUpdate"];
                appointmentData.Rows[i]["lastUpdate"] = lastUpdate.ToLocalTime();
            }

            return appointmentData;
        }

        //Methods for Clicking Customer buttons

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            new AddCustomer().ShowDialog();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            int currentCustomerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells[0].Value);

            Customer currentCustomer = Customer.GetCustomer(currentCustomerId);

            Address currentAddress = Address.GetAddress(currentCustomer.AddressId);

            City currentCity = City.GetCity(currentAddress.CityId);

            Country currentCountry = Country.GetCountry(currentCity.CountryId);

            new UpdateCustomer(currentCustomer, currentAddress, currentCity, currentCountry).ShowDialog();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            int rowId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells[0].Value);
            Customer.DeleteCustomer(rowId);
            RefreshCustomerGrid();
        }
        
        //Method to refresh Customer DataGridView

        public void RefreshCustomerGrid()
        {
            customerData.Clear();
            dgvCustomers.DataSource = getCustomers();
        }

        //Methods for clicking appointment buttons

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            new AddAppointment().ShowDialog();
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = Appointment.GetAppointment(Convert.ToInt32(dgvAppointments.CurrentRow.Cells["appointmentId"].Value));
            new UpdateAppointment(appointment).ShowDialog();
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            Appointment.DeleteAppointment(Convert.ToInt32(dgvAppointments.CurrentRow.Cells["appointmentId"].Value));
            RefreshAppointments();
        }

        //Methods to change appointment view by all, month, or week

        private void rbAllAppointments_CheckedChanged(object sender, EventArgs e)
        {
            dgvAppointments.DataSource = getAllAppointments();
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            appointmentData.Clear();
            dgvAppointments.DataSource = getMonthsAppointments();
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            appointmentData.Clear();
            dgvAppointments.DataSource = getWeeksAppointments();
        }

        //Method to refresh appointment grid

        public void RefreshAppointments()
        {
            appointmentData.Clear();

            if (rbAllAppointments.Checked == true)
            {
                dgvAppointments.DataSource = getAllAppointments();
            }
            if (rbMonth.Checked == true)
            {
                dgvAppointments.DataSource = getMonthsAppointments();
            }
            if (rbWeek.Checked == true)
            {
                dgvAppointments.DataSource = getWeeksAppointments();
            }
        }
    }
}
