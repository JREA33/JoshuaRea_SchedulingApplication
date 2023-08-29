using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication.Classes
{
    public class Country
    {
        //Setup Attributes

        private int countryId;
        private string country;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Create Getters and Setters

        public int CountryId
        {
            get => countryId;
            set => countryId = value;
        }
        public string CountryName
        {
            get => country;
            set => country = value;
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

        //Create Constructors

        public Country() { }
        public Country(int countryId, string country, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            this.countryId = countryId;
            this.country = country;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }

        public Country(string country)
        {
            this.country = country;
        }

        //Setup Methods

        //Get a New CountryID

        public static void GetNewCountryID(Country country)
        {
            string queryString = "SELECT MAX(countryId) AS 'newId' FROM country";
            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                country.countryId = Convert.ToInt32(rdr["newId"]) + 1;
            }
            rdr.Close();
        }

        //Get or Create a Country

        public static void GetOrCreateCountry(Country updatedCountry)
        {
            bool countryExists = false;
            string existsString = "SELECT * FROM country WHERE country = " + $"'{updatedCountry.country}'";
            MySqlCommand cmd = new MySqlCommand(existsString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr.HasRows)
            {
                countryExists = true;
                updatedCountry.countryId = Convert.ToInt32(rdr["countryId"]);

            }
            rdr.Close();
            if (countryExists == false)
            {
                string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                GetNewCountryID(updatedCountry);

                string queryString = "INSERT INTO country " +
                    $"VALUES ('{updatedCountry.countryId}', '{updatedCountry.country}', '{currentDateTime}', '{Login.currentUsername}', '{currentDateTime}', '{Login.currentUsername}')";
                MySqlCommand cmd2 = new MySqlCommand(queryString, DBConnection.conn);
                cmd2.ExecuteNonQuery();
            }
        }

        public static Country GetCountry(int countryId)
        {
            Country country = new Country();

            string query = "SELECT * FROM country WHERE countryId = " + countryId;

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                country.countryId = Convert.ToInt32(rdr["countryId"]);
                country.country = rdr["country"].ToString();
                country.createDate = Convert.ToDateTime(rdr["createDate"]);
                country.createdBy = rdr["createdBy"].ToString();
                country.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]);
                country.lastUpdatedBy = rdr["lastUpdateBy"].ToString();
            }
            rdr.Close();

            return country;
        }
    }
}
