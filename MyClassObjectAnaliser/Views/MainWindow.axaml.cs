using Avalonia.Controls;
using MyClassObjectAnaliser.ViewModels;

namespace MyClassObjectAnaliser.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
