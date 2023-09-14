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
    public class Customer
    {
        //Setup Attributes

        private int customerId;
        private string customerName;
        private int addressId;
        private int active;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdatedBy;

        //Setup Getters/Setters

        public int CustomerId
        {
            get => customerId;
            set => customerId = value;
        }
        public string CustomerName
        {
            get => customerName;
            set => customerName = value;
        }
        public int AddressId
        {
            get => addressId;
            set => addressId = value;
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

        //Setup Constructors

        public Customer() { }

        public Customer(int customerId, string customerName, int addressId, int active)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.addressId = addressId;
            this.active = active;
        }
        public Customer(int customerId, string customerName, int addressId, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.addressId = addressId;
            this.active = active;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }

        //Class Methods

        public static void CreateCustomer(Customer newCustomer)
        {
            string currentTimestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string queryString = "INSERT INTO Customer " +
                $"VALUES ('{newCustomer.customerId}', '{newCustomer.customerName}', '{newCustomer.addressId}', '{newCustomer.active}', '{currentTimestamp}', '{Login.currentUsername}', '{currentTimestamp}', '{Login.currentUsername}')";
            MySqlCommand cmd2 = new MySqlCommand(queryString, DBConnection.conn);
            cmd2.ExecuteNonQuery();
        }
        
        public static int getNewCustomerID()
        {
            int newID = 0;

            string queryString = "SELECT MAX(customerId) AS 'newId' FROM customer";
            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newID = Convert.ToInt32(rdr[0]);
                newID += 1;
            }
            rdr.Close();

            return newID;
        }

        public static void DeleteCustomer(int customerId)
        {
            string query = "DELETE FROM customer WHERE customerId = " + $"'{customerId}'";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

        public static Customer GetCustomer(int customerId)
        {
            Customer customer = new Customer();

            string queryString = "SELECT * FROM customer WHERE customerId = " + customerId;

            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                customer.customerId = Convert.ToInt32(rdr["customerId"]);
                customer.customerName = rdr["customerName"].ToString();
                customer.addressId = Convert.ToInt32(rdr["addressId"]);
                customer.active = Convert.ToInt32(rdr["active"]);
                customer.createDate = Convert.ToDateTime(rdr["createDate"]).ToLocalTime();
                customer.createdBy = rdr["createdBy"].ToString();
                customer.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]).ToLocalTime();
                customer.lastUpdatedBy = rdr["lastUpdateBy"].ToString();
            }
            rdr.Close();

            return customer;     
        }

        public static void UpdateCustomer(Customer updatedCustomer)
        {
            string query = "UPDATE customer " +
                "SET " +
                $"customerName = '{updatedCustomer.customerName}', " +
                $"addressId = '{updatedCustomer.addressId}', " +
                $"active = '{updatedCustomer.active}', " +
                $"lastUpdate = '{updatedCustomer.lastUpdate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdateBy = '{Login.currentUsername}'" +
                $"WHERE customerId = '{updatedCustomer.customerId}';";

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }
    }
}
