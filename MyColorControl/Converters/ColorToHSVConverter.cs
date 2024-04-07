using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace MyColorControl.Converters
{
    internal class ColorToHSVConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.ToHsv();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

