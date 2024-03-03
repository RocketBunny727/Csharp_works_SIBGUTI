using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace Homework_5
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
                if (selectedItem.Type == "image")
                {
                explorer.ChoosenImage = selectedItem;
                }
            }
        }

        private void DoubleTappedListBox(object? sender, TappedEventArgs args)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is FileSystemItem selectedItem)
            {
                explorer.ChoosenItem = selectedItem;
            }
        }

    }
}