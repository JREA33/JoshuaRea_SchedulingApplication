using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoshuaRea_SchedulingApplication.Database;
using MySql.Data.MySqlClient;

namespace JoshuaRea_SchedulingApplication
{
    public class Address
    {

        //Setup Attributes

        private int addressId;
        private string address;
        private string address2;
        private int cityId;
        private string postalCode;
        private string phone;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Create Getters/Setters

        public int AddressId
        {
            get => addressId;
            set => addressId = value;
        }
        public string Address1
        {
            get => address;
            set => address = value;
        }
        public string Address2
        {
            get => address2;
            set => address2 = value;
        }
        public int CityId
        {
            get => cityId;
            set => cityId = value;
        }
        public string PostalCode
        {
            get => postalCode;
            set => postalCode = value;
        }
        public string Phone
        {
            get => phone;
            set => phone = value;
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

        public Address() { }
        public Address(int addressId, string address, string address2, int cityId, string postalCode, string phone, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            this.addressId = addressId;
            this.address = address;
            this.address2 = address2;
            this.cityId = cityId;
            this.postalCode = postalCode;
            this.phone = phone;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }

        public Address(string address, string address2, int cityId, string postalCode, string phone)
        {
            this.address = address;
            this.address2 = address2;
            this.cityId = cityId;
            this.postalCode = postalCode;
            this.phone = phone;
        }

        //Setup Methods

        public static void GetOrCreateAddress(Address newAddress)
        {
            bool addressExists = false;

            string existsString = 
                "SELECT * " +
                "FROM address " +
                "WHERE " +
                "address.address = " + $"'{newAddress.address}' and " +
                "address.address2 = " + $"'{newAddress.address2}' and " +
                "address.cityId = " + $"'{newAddress.cityId}' and " + 
                "address.postalCode = " + $"'{newAddress.postalCode}' and " + 
                "address.phone = " + $"'{newAddress.phone}'";
            MySqlCommand cmd = new MySqlCommand(existsString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr.HasRows)
            {
                addressExists = true;
                newAddress.addressId = Convert.ToInt32(rdr["addressId"]);
                newAddress.address = rdr["address"].ToString();
                newAddress.address2 = rdr["address2"].ToString();
                newAddress.cityId = Convert.ToInt32(rdr["cityId"]);
                newAddress.postalCode = rdr["postalCode"].ToString();
                newAddress.phone = rdr["phone"].ToString();
                newAddress.createDate = Convert.ToDateTime(rdr["createDate"]).ToLocalTime();
                newAddress.createdBy = rdr["createdBy"].ToString();
                newAddress.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]).ToLocalTime();
                newAddress.lastUpdatedBy = rdr["lastUpdateBy"].ToString();

            }
            rdr.Close();

            if (addressExists == false)
            {
                string currentTimestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                newAddress = GetNewAddressID(newAddress);
                string queryString = "INSERT INTO address " + 
                    $"VALUES ('{newAddress.addressId}', '{newAddress.address}', '{newAddress.address2}', '{newAddress.cityId}', '{newAddress.postalCode}', '{newAddress.phone}', '{currentTimestamp}', '{Login.currentUsername}', '{currentTimestamp}', '{Login.currentUsername}')";
                MySqlCommand cmd2 = new MySqlCommand(queryString, DBConnection.conn);
                cmd2.ExecuteNonQuery();
            }
        }

        public static Address GetNewAddressID(Address newAddress)
        {

            string queryString = "SELECT MAX(addressId) AS 'newId' FROM address";
            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newAddress.addressId = Convert.ToInt32(rdr["newId"]) + 1;
            }
            rdr.Close();

            return newAddress;
        }

        public static Address GetAddress(int addressId)
        {
            Address currentAddress = new Address();
            
            string query = "SELECT * FROM address WHERE addressId = " + addressId;

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                currentAddress.addressId = Convert.ToInt32(rdr["addressId"]);
                currentAddress.address = rdr["address"].ToString();
                currentAddress.address2 = rdr["address2"].ToString();
                currentAddress.cityId = Convert.ToInt32(rdr["cityId"]);
                currentAddress.postalCode = rdr["postalCode"].ToString();
                currentAddress.phone = rdr["phone"].ToString();
                currentAddress.createDate = Convert.ToDateTime(rdr["createDate"]).ToLocalTime();
                currentAddress.createdBy = rdr["createdBy"].ToString();
                currentAddress.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]).ToLocalTime();
                currentAddress.lastUpdatedBy = rdr["lastUpdateBy"].ToString();
            }
            rdr.Close();

            return currentAddress;
        }
    }
}
