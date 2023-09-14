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
        public static string currentUsername;
        public Login()
        {
            InitializeComponent();

            //CultureInfo.CurrentCulture = new CultureInfo("es");

            setLanguage();
        }

        //Method to change the language according to the language set in Windows.

        private void setLanguage()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "es")
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

        //Click of the login button

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

        //Exit Button Click

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
