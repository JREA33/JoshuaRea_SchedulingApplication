using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication.Classes
{
    public class City
    {
        //Setup Attributes

        private int cityId;
        private string city;
        private int countryId;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Create Getters and Setters
        public int CityId
        {
            get => cityId;
            set => cityId = value;
        }
        public string CityName
        {
            get => city;
            set => city = value;
        }
        public int CountryId
        {
            get => countryId;
            set => countryId = value;
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

        public City() { }
        public City(int cityId, string city, int countryId, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            this.cityId = cityId;
            this.city = city;
            this.countryId = countryId;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }

        public City(string city, int countryId)
        {
            this.city = city;
            this.countryId = countryId;
        }

        //Setup Methods

        //Create New City
        public static void GetOrCreateCity(City city)
        {
            bool cityExists = false;

            //Check if City Exists

            string existsString = $"SELECT * FROM city WHERE city = '{city.city}' and countryId = '{city.countryId}'";
            MySqlCommand cmd = new MySqlCommand(existsString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr.HasRows)
            {
                cityExists = true;
                city.cityId = Convert.ToInt32(rdr["cityId"]);

            }
            rdr.Close();
            if (cityExists == false)
            {
                //Create New City if it doesn't exist

                //Get a current Timestamp
                string currentTimestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");

                //Assign CityId to class
                GetNewCityID(city);

                //Create SQL string to insert new record
                string queryString = "INSERT INTO city " +
                    $"VALUES ('{city.cityId}', '{city.city}', '{city.countryId}', '{currentTimestamp}', '{Login.currentUsername}', '{currentTimestamp}', '{Login.currentUsername}')";
                
                //Create and execute SQL command
                MySqlCommand cmd2 = new MySqlCommand(queryString, DBConnection.conn);
                cmd2.ExecuteNonQuery();
            }
        }


        //Input a city and get a new cityId
        public static void GetNewCityID(City newCity)
        {
            string queryString = "SELECT MAX(cityId) AS 'newId' FROM city";
            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newCity.cityId = Convert.ToInt32(rdr["newId"]) + 1;
            }
            rdr.Close();
        }

        public static City GetCity(int cityId)
        {
            City city = new City();

            string query = "SELECT * FROM city WHERE cityId = " + cityId;

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                city.cityId = Convert.ToInt32(rdr["cityId"]);
                city.city = rdr["city"].ToString();
                city.countryId = Convert.ToInt32(rdr["countryId"]);
                city.createDate = Convert.ToDateTime(rdr["createDate"]).ToLocalTime();
                city.createdBy = rdr["createdBy"].ToString();
                city.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]).ToLocalTime();
                city.lastUpdatedBy = rdr["lastUpdateBy"].ToString();
            }
            rdr.Close();

            return city;
        }
    }
}
