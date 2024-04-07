using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MyColorControl.Models;

namespace MyColorControl.ViewModels
{
    public partial class ColorControlViewModel : ObservableObject
    {
        private ColorControl _palette;

        public ColorControl Palette
        {
            get { return _palette; }
            set { _palette = value; }
        }

        public ColorControlViewModel()
        {
            Palette = new ColorControl();
        }

        public void ChangeSelectedColor(IBrush fill)
        {
            Color RectangleColor;

            IBrush brush = fill;

            if (brush is ISolidColorBrush solidColorBrush)
            {
                RectangleColor = solidColorBrush.Color;
                Palette.SelectedColor = RectangleColor;
                Palette.SelectedHSVColor = Palette.SelectedColor.ToHsv();
            }
        }
    }
}
