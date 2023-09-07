using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication.Forms
{
    public partial class UpdateAppointment : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        DataTable users = new DataTable();

        public UpdateAppointment(Appointment appointment)
        {
            InitializeComponent();

            //Populate Customer Combo Box with Values
            foreach (DataGridViewRow row in main.dgvCustomers.Rows)
            {
                cmbCustomer.Items.Add(row.Cells["customerName"].Value.ToString());
            }

            //Populate User Combo Box with Values
            string query = "SELECT * FROM user;";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(users);

            foreach (DataRow row in users.Rows)
            {
                cmbUsers.Items.Add(row["userName"].ToString());
            }

            //Populate fields on page
            txtAppointmentID.Text = appointment.AppointmentId.ToString();
            Customer customer = Customer.GetCustomer(appointment.CustomerId);
            cmbCustomer.Text = customer.CustomerName;
            User user = User.GetUser(appointment.AppointmentId);
            cmbUsers.Text = user.UserName;
            txtTitle.Text = appointment.Title;
            txtDescription.Text = appointment.Description;
            txtLocation.Text = appointment.Location;
            txtContact.Text = appointment.Contact;
            txtType.Text = appointment.Type;
            txtURL.Text = appointment.URL;
            dateStart.Value = appointment.Start;
            dateEnd.Value = appointment.End;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }
    }
}
