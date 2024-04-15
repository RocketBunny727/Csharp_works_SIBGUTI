using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyClassObjectAnaliser.Models
{
    public partial class Geo : ObservableObject
    {
        [ObservableProperty] private string? lat;
        [ObservableProperty] private string? lng;
    }
}
