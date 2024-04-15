using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyClassObjectAnaliser.Models
{
    public partial class Address : ObservableObject
    {
        [ObservableProperty] private string? _street;
        [ObservableProperty] private string? _suite;
        [ObservableProperty] private string? _city;
        [ObservableProperty] private string? _zipcode;
        [ObservableProperty] private Geo? _geo;
    }
}
