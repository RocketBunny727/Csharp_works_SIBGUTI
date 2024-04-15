using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyClassObjectAnaliser.Models
{
    public partial class Car : ObservableObject
    {
        [ObservableProperty] private string? _carName;
        [ObservableProperty] private string? _color;
        [ObservableProperty] private int _year;
        [ObservableProperty] private int _age;
        [ObservableProperty] private string? _price;
        [ObservableProperty] private double _mileage;
    }
}
