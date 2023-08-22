using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        string loginError = "Username and Password do not match.";
        public Login()
        {
            InitializeComponent();
            MySqlConnection c = DBConnection.conn;

            setLanguage();
        }

        private void setLanguage()
        {
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es")
            {
                lblLogin.Text = "Acceso";
                lblUsername.Text = "Nombre de Usuario";
                lblPassword.Text = "Contrasena";
                btnLogin.Text = "Acceso";
                btnExit.Text = "Salida";
                this.Text = "Inicio de Sesion del Programador";
                loginError = "Nombre de usuario y contrasena no coincidem.";
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool foundUser = User.FindUser(txtUserName.Text, txtPassword.Text);

            if (foundUser == true)
            {
                this.Hide();
                new Main().ShowDialog();
                this.Close();
            }
            else
            {
            MessageBox.Show(loginError);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
