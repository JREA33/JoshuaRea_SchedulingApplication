using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public class User
    {
        //Create Attributes
        private string userName;
        private string password;

        //Create Getters and Setters
        public string UserName 
        { 
            get {return userName;}
            set {userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //Methods
        public static void FindUser(string userName, string password)
        {
            MySqlConnection c = DBConnection.conn;
            string sqlString = $"SELECT * FROM user WHERE userName = '{userName}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(sqlString, c);
            MySqlDataReader readRows = cmd.ExecuteReader();
            
            if (readRows.HasRows == false)
            {
                MessageBox.Show("Username and Password do not match.");
                readRows.Close();
            }
            else
            {
                readRows.Read();
                userName = readRows[1].ToString();
                password = readRows[2].ToString();
                readRows.Close();
            }
        }
    }
}
