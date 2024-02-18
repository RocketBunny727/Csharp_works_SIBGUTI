using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Calc;
using System;

namespace Homework_3
{
    public partial class MainWindow : Window
    {
        private MyCalculator _calculator;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new MyCalculator();
            _calculator.Init(this.FindControl<TextBlock>("Display"), this.FindControl<TextBlock>("Console"));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs args)
        {
            var button = (Button)sender;
            if (button == null) return;

            string content = button.Content.ToString();
            if (string.IsNullOrEmpty(content)) return;

            if (content == "=")
            {
                _calculator.ExecuteLastOperation();
                return;
            }

            if (content == "C")
            {
                _calculator.Clear();
                return;
            }

            if (content == "Backspace")
            {
                _calculator.RemoveLastDigit();
                return;
            }

            _calculator.HandleInput(content);
        }
    }
}
