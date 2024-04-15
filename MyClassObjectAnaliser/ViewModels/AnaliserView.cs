using Avalonia.Controls;
using MyClassObjectAnaliser.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MyClassObjectAnaliser.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyClassObjectAnaliser.ViewModels
{
    public partial class AnaliserView : ObservableObject
    {
        [ObservableProperty] private User? user;
        [ObservableProperty] TreeView? _treeView;
        private ObservableCollection<object> _treeData = new ObservableCollection<object>();
        public ObservableCollection<object> TreeData
        {
            get => _treeData;
            set => SetProperty(ref _treeData, value);
        }
        public AnaliserView()
        {
            user = new User
            {
                Id = 1,
                Name = "James",
                Username = "Evil_Blackberry",
                Email = "HandsomeGuy@gmail.com",
                Phone = "123456789",
                Website = "www.only_a_man.com",
                Address = new Address
                {
                    Street = "3rd Avenue",
                    Suite = "Apt 101",
                    City = "Los Angeles",
                    Zipcode = "12345",
                    Geo = new Geo
                    {
                        Lat = "40.7128",
                        Lng = "-74.0060"
                    }
                },
                Company = new Company
                {
                    Name = "Plague Inc.",
                    CatchPhrase = "Making the world a better place",
                    Bs = "It's just a plague"
                },

                Car = new Car
                {
                    CarName = "Audi RS6 Sportback",
                    Color = "Olive",
                    Year = 2022,
                    Age = 2,
                    Price = "110000$ (USD)",
                    Mileage = 4000
                }
            };
        }
    }
}
