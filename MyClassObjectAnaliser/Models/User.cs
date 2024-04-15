using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyClassObjectAnaliser.Models
{
    public partial class User : ObservableObject
    {

        [ObservableProperty] private int _id;
        [ObservableProperty] private string? _name;
        [ObservableProperty] private string? _username;
        [ObservableProperty] private string? _email;
        [ObservableProperty] private string? _phone;
        [ObservableProperty] private string? _website;
        [ObservableProperty] private Address? _address;
        [ObservableProperty] private Company? _company;
        [ObservableProperty] private Car? _car;
    }
}
