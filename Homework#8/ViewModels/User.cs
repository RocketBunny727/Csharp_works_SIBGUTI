using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8.ViewModels
{
    public partial class User : ObservableObject
    {

        [ObservableProperty]
        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        [ObservableProperty]
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        [ObservableProperty]
        private string _username;
        public string username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }

        [ObservableProperty]
        private string _email;
        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        [ObservableProperty]
        private string _phone;
        public string phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
            }
        }

        [ObservableProperty]
        private string _website;
        public string website
        {
            get { return _website; }
            set
            {
                _website = value;
            }
        }

        [ObservableProperty]
        private Address _address;
        public Address address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        [ObservableProperty]
        private Company _company;
        public Company company
        {
            get { return _company; }
            set
            {
                _company = value;
            }
        }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }
    }

    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }
}