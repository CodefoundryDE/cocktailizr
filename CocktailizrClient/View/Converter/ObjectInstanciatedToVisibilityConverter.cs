using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CocktailizrClient.View.Converter
{
    public class ObjectInstanciatedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value != null;

            return !boolValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
