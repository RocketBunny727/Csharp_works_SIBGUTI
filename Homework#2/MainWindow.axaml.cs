using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.ComponentModel;
using Avalonia.Controls.Shapes;



namespace Homework_2
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            rectangle = this.FindControl<Rectangle>("rectangle");
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (Button)sender;
            rectangle.Fill = button.Background;
        }

    }
}