﻿using System;
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
        private int userId;
        private string userName;
        private string password;
        private int active;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Create Getters and Setters

        public int UserId
        {
            get => userId;
            set => userId = value;
        }
        public string UserName 
        { 
            get => userName;
            set => userName = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }
        public int Active
        {
            get => active;
            set => active = value;
        }
        public DateTime CreateDate
        {
            get => createDate;
            set => createDate = value;
        }
        public string CreatedBy
        {
            get => createdBy;
            set => createdBy = value;
        }
        public DateTime LastUpdate
        {
            get => lastUpdate;
            set => lastUpdate = value;
        }
        public string LastUpdatedBy
        {
            get => lastUpdatedBy;
            set => lastUpdatedBy = value;
        }

        //Methods
        public static bool FindUser(string userName, string password)
        {
            MySqlConnection c = DBConnection.conn;
            string sqlString = $"SELECT userName FROM user WHERE userName = '{userName}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(sqlString, DBConnection.conn);
            MySqlDataReader readRows = cmd.ExecuteReader();
            readRows.Read();

            if (readRows.HasRows == false)
            {
                readRows.Close();
                return false;
            }
            else
            {
                Login.currentUsername = readRows[0].ToString();
                readRows.Close();
                return true;
            }
        }
    }
}