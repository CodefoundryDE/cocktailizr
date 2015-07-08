using System;
using System.Windows.Data;

namespace CocktailizrClient.View.Converter
{
    public class IsNotZeroConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return ((int) value) != 0;
            }
            else if (value is double)
            {
                return !((double) value).Equals(0.0);
            }
            else if (value is Guid)
            {
                return !((Guid) value).Equals(Guid.Empty);
            }
            else if (value is float)
            {
                return !((float) value).Equals(0);
            }
            else if (value is decimal)
            {
                return !((decimal) value).Equals(0);
            }
            return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}