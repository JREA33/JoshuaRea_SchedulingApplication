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
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Get the connection string.
            string constr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            //Make the connection.
            MySqlConnection conn = null;

            //Open the connection
            try
            {
                conn = new MySqlConnection(constr);

                //Open the connection
                conn.Open();

                MessageBox.Show("Connection is Open.");
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the connection
                if(conn!= null)
                {
                    conn.Close();
                }
            }
        }
    }
}
