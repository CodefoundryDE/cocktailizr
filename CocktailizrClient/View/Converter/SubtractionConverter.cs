using System.Windows.Data;

namespace CocktailizrClient.View.Converter
{
    public class SubtractionConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal divisor;
            if (parameter == null || !decimal.TryParse(parameter.ToString(), out divisor))
            {
                return 0;
            }
            if (value is int)
            {
                return (double)((int)value - divisor);
            }
            else if (value is double)
            {
                return ((double)value - (double)divisor);
            }
            else if (value is float)
            {
                return ((float)value - (float)divisor);
            }
            else if (value is decimal)
            {
                return (decimal)((decimal)value - divisor);
            }
            return 0;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}