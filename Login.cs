using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            MySqlConnection c = DBConnection.conn;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User.FindUser(txtUserName.Text, txtPassword.Text);
        }
    }
}
