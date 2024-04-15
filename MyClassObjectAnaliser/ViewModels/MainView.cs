using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassObjectAnaliser.ViewModels;
using MyClassObjectAnaliser.Views;
using MyClassObjectAnaliser.Views.Controls;
using MyClassObjectAnaliser.Models;

namespace MyClassObjectAnaliser.ViewModels
{
    public partial class MainView
    {
        private AnaliserView? _view;
        private AnaliserControl? _control;
        public MainView()
        {
            _view = new AnaliserView();
        }
    }
}
