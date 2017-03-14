using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Zza.Data
{
    public class Customer : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _phone;

        public Customer()
        {
            Orders = new List<Order>();
        }
        [Key]
        public Guid Id { get; set; }
        public Guid? StoreId { get; set; }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<Order> Orders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
