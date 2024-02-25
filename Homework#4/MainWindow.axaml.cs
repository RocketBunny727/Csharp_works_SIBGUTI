using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace Homework_4
{
    public partial class MainWindow : Window
    {
        private readonly Explorer explorer;

        public MainWindow()
        {
            InitializeComponent();
            explorer = new Explorer();
            DataContext = explorer;
        }

        private void TappedListBox(object? sender, TappedEventArgs args)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is FileSystemItem selectedItem)
            {
                explorer.ChoosenItem = selectedItem;
            }
        }
    }
}
