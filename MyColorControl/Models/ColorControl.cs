using Avalonia.Media.Imaging;
using Avalonia.Media;
using System;
using System.Collections.ObjectModel; // Импорт пространства имён
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using MyColorControl.Converters;
using System.Collections.Generic;

namespace MyColorControl.Models
{
    public partial class ColorControl : ObservableObject
    {
        [ObservableProperty] private Bitmap _buttonIcon;
        [ObservableProperty] private bool _isVisible;

        private List<Color> _mainColors = new List<Color>(new[]
                    {
                "#FF0000", "#FFA500", "#FFFF00", "#008000", "#0000FF", "#800080", "#FFC0CB", "#FFD700",
                "#808080", "#FFFFFF", "#000000", "#FF1493", "#FF6347", "#FF4500", "#ADFF2F", "#008080",
                "#4B0082", "#9932CC", "#FF69B4", "#FFA07A", "#FFFACD", "#F0E68C", "#B0C4DE", "#4682B4",
                "#D2B48C", "#228B22", "#FFDAB9", "#FAFAD2", "#E0FFFF", "#FFE4B5", "#FFF8DC", "#B0E0E6",
                "#CD5C5C", "#FF7F50", "#7FFFD4", "#8A2BE2", "#20B2AA", "#40E0D0", "#00FF7F", "#008B8B",
                "#8B0000", "#800000", "#808000", "#00FF00", "#00FA9A", "#00FFFF", "#7FFF00", "#66CDAA",
                "#00CCFF", "#D000FF"
            }.Select(Color.Parse));

        private ObservableCollection<Color> _userColors;
        private Color _selectedColor;
        private HsvColor _selectedHSVColor;

        public RelayCommand<object> ToggleVisibilityCommand { get; }
        public RelayCommand<object> AddUserColorCommand { get; }

        public List<Color> MainColors
        {
            get { return _mainColors; }
        }

        public ObservableCollection<Color> UserColors
        {
            get { return _userColors; }
            set { SetProperty(ref _userColors, value); }
        }
        public Color SelectedColor
        {
            get { return _selectedColor;  }
            set
            {
                _selectedColor = value;
                SelectedHSVColor = SelectedColor.ToHsv();
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        public HsvColor SelectedHSVColor
        {
            get { return _selectedHSVColor;  }
            set
            {
                _selectedHSVColor = value;
                OnPropertyChanged(nameof(SelectedHSVColor));
            }
        }

        public ColorControl()
        {
            IsVisible = false;
            ToggleVisibilityCommand = new RelayCommand<object>(ToggleVisibility);
            AddUserColorCommand = new RelayCommand<object>(AddUserColor);

            if (IsVisible)
                ButtonIcon = new Bitmap("~/../../../../Assets/close.png");
            else
                ButtonIcon = new Bitmap("~/../../../../Assets/open.png");

            UserColors = new ObservableCollection<Color>(Enumerable.Repeat(default(Color), 54));
            SelectedColor = Color.Parse("#FFFFFF");
            SelectedHSVColor = SelectedColor.ToHsv();
        }

        void ToggleVisibility(object parameter)
        {
            IsVisible = !IsVisible;
            if (IsVisible)
                ButtonIcon = new Bitmap("~/../../../../Assets/close.png");
            else
                ButtonIcon = new Bitmap("~/../../../../Assets/open.png");
        }

        void AddUserColor(object parameter)
        {
            int index = UserColors.IndexOf(default(Color));
            if (index != -1)
            {
                UserColors[index] = SelectedColor;
            }
            else
            {
                Console.WriteLine("Невозможно добавить цвет, так как список полностью заполнен.");
            }
        }
    }
}
