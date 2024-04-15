using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyClassObjectAnaliser.Models
{
    public partial class Company : ObservableObject
    {
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _catchPhrase;
        [ObservableProperty] private string _bs;
    }
}
