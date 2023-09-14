using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public class Appointment
    {
        //Setup Attributes

        private int appointmentId;
        private int customerId;
        private int userId;
        private string title;
        private string description;
        private string location;
        private string contact;
        private string type;
        private string url;
        private DateTime start;
        private DateTime end;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Setup Getters/Setters
        public int AppointmentId
        {
            get => appointmentId;
            set => appointmentId = value;
        }
        public int CustomerId
        {
            get => customerId;
            set => customerId = value;
        }
        public int UserId
        {
            get => userId;
            set => userId = value;
        }
        public string Title
        {
            get => title;
            set => title = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
        public string Location
        {
            get => location;
            set => location = value;
        }
        public string Contact
        {
            get => contact;
            set => contact = value;
        }
        public string Type
        {
            get => type;
            set => type = value;
        }
        public string URL
        {
            get => url;
            set => url = value;
        }
        public DateTime Start
        {
            get => start;
            set => start = value;
        }
        public DateTime End
        {
            get => end;
            set => end = value;
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

        //Setup Constructors

        public Appointment() { }
        public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            this.appointmentId = appointmentId;
            this.customerId = customerId;
            this.userId = userId;
            this.title = title;
            this.description = description;
            this.location = location;
            this.contact = contact;
            this.type = type;
            this.url = url;
            this.start = start;
            this.end = end;
        }

        public static Appointment GetAppointment(int appointmentID)
        {
            string query = $"SELECT * FROM appointment WHERE appointmentId = '{appointmentID}'";

            Appointment appointment = new Appointment();

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                appointment.appointmentId = Convert.ToInt32(rdr["appointmentId"]);
                appointment.customerId = Convert.ToInt32(rdr["customerId"]);
                appointment.userId = Convert.ToInt32(rdr["userId"]);
                appointment.title = rdr["title"].ToString();
                appointment.description = rdr["description"].ToString();
                appointment.location = rdr["location"].ToString();
                appointment.contact = rdr["contact"].ToString();
                appointment.type = rdr["type"].ToString();
                appointment.url = rdr["url"].ToString();
                appointment.start = Convert.ToDateTime(rdr["start"]).ToLocalTime();
                appointment.end = Convert.ToDateTime(rdr["end"]).ToLocalTime();
                appointment.createDate = Convert.ToDateTime(rdr["createDate"]).ToLocalTime();
                appointment.createdBy = rdr["createdBy"].ToString();
                appointment.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]).ToLocalTime();
                appointment.LastUpdatedBy = rdr["lastUpdateBy"].ToString();

            }

            rdr.Close();

            return appointment;
        }

        public static int GetNewAppointmentID()
        {
            int newId = 0;

            string query = "SELECT MAX(appointmentId) AS 'newId' FROM appointment";

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                newId = Convert.ToInt32(rdr["newId"]) + 1;
            }
            rdr.Close();

            return newId;
        }

        public static void CreateAppointment(Appointment newAppointment)
        {
            string currentTimestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string queryString = "INSERT INTO appointment " +
                $"VALUES ('{newAppointment.appointmentId}', '{newAppointment.customerId}', '{newAppointment.userId}', '{newAppointment.title}', '{newAppointment.description}', '{newAppointment.location}', '{newAppointment.contact}', '{newAppointment.type}', '{newAppointment.URL}', '{newAppointment.start.ToString("yyyy-MM-dd HH:mm:ss")}', '{newAppointment.end.ToString("yyyy-MM-dd HH:mm:ss")}', '{currentTimestamp}', '{Login.currentUsername}', '{currentTimestamp}', '{Login.currentUsername}')";
            MySqlCommand cmd2 = new MySqlCommand(queryString, DBConnection.conn);
            cmd2.ExecuteNonQuery();
        }

        public static void DeleteAppointment(int appointmentID)
        {
            string query = $"DELETE FROM appointment WHERE appointmentId = '{appointmentID}'";

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

        public static void UpdateAppointment(Appointment appointment)
        {
            string query = "UPDATE appointment " +
                "SET " +
                $"customerId = '{appointment.customerId}', " +
                $"userId = '{appointment.userId}', " +
                $"title = '{appointment.title}', " +
                $"description = '{appointment.description}', " +
                $"location = '{appointment.location}', " +
                $"contact = '{appointment.contact}', " +
                $"type = '{appointment.type}', " +
                $"url = '{appointment.url}', " +
                $"start = '{appointment.start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"end = '{appointment.end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdateBy = '{Login.currentUsername}'" +
                $"WHERE appointmentId = '{appointment.appointmentId}';";

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }
    }
}
