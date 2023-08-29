using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
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
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }
    }
}
