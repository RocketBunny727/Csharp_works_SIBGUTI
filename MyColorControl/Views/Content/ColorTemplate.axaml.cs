using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using MyColorControl.Models;
using Avalonia.Media;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MyColorControl.ViewModels;

namespace MyColorControl.Views.Content;

public partial class ColorTemplate : UserControl
{
    public ColorControlViewModel ViewModel;

    public ColorTemplate()
    {
        InitializeComponent();
        ViewModel = new ColorControlViewModel();
        DataContext = ViewModel;
    }

    private void SelectedColorTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender is Rectangle rectangle && rectangle.Fill != null)
        {
            ViewModel.ChangeSelectedColor(rectangle.Fill);
        }
    }

}