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

namespace JoshuaRea_SchedulingApplication
{
    public partial class Main : Form
    {
        public static MySqlConnection c = DBConnection.conn;
        public Main()
        {
            InitializeComponent();
            dgvCustomers.DataSource = getCustomers();
        }

        public static DataTable getCustomers()
        {
            string sqlString = "SELECT * FROM customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, c);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable customers = new DataTable();
            adapter.Fill(customers);
            return customers;
        }
    }
}
