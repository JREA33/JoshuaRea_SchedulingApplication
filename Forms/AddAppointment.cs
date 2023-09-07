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
    public partial class AddAppointment : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        DataTable users = new DataTable();
        public AddAppointment()
        {
            InitializeComponent();

            BtnSaveDisable();

            //Get a New Appointment ID when Page is Initialized
            txtAppointmentID.Text = Appointment.GetNewAppointmentID().ToString();

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

        }
        private void BtnSaveDisable()
        {
            if (
                string.IsNullOrWhiteSpace(txtAppointmentID.Text) ||
                string.IsNullOrWhiteSpace(cmbCustomer.Text) ||
                string.IsNullOrWhiteSpace(cmbUsers.Text)
                )
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCustomer.Text = cmbCustomer.SelectedItem.ToString();
            BtnSaveDisable();
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUsers.Text = cmbUsers.SelectedItem.ToString();
            BtnSaveDisable();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string customerID = "";

            int userID = 0;

            foreach (DataGridViewRow row in main.dgvCustomers.Rows)
            {
                if (row.Cells["customerName"].Value.ToString().Equals(cmbCustomer.Text))
                {
                    customerID = row.Cells["customerId"].Value.ToString();
                }
            }

            foreach (DataRow row in users.Rows)
            {
                if (row.Field<string>("userName").Equals(cmbUsers.Text))
                {
                    userID = row.Field<int>("userId");
                }
            }

            Appointment newAppointment = new Appointment();

            newAppointment.AppointmentId = Convert.ToInt32(txtAppointmentID.Text);
            newAppointment.CustomerId = Convert.ToInt32(customerID);
            newAppointment.UserId = Convert.ToInt32(userID);
            newAppointment.Title = txtTitle.Text;
            newAppointment.Description = txtDescription.Text;
            newAppointment.Location = txtLocation.Text;
            newAppointment.Contact = txtContact.Text;
            newAppointment.Type = txtType.Text;
            newAppointment.URL = txtURL.Text;
            newAppointment.Start = dateStart.Value;
            newAppointment.End = dateEnd.Value;

            Appointment.CreateAppointment(newAppointment);

            main.RefreshAppointments();

            this.Close();
        }

    }
}
