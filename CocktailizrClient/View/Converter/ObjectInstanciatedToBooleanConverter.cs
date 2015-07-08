using System;
using System.Globalization;
using System.Windows.Data;

namespace CocktailizrClient.View.Converter
{
    public class ObjectInstanciatedToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
