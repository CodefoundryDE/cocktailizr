using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CocktailizrClient.View.Converter
{
    /// <summary>
    /// Konvertiert einen Boolean-Wert zu einem Brush
    /// </summary>
    /// <remarks>
    /// Es wird versucht, den ConverterParameter zu einem Color-Wert zu casten.
    /// </remarks>
    public class BooleanToBrushConverter : IValueConverter
    {
        private static readonly Color TransparentColor = Color.FromArgb(0, 0, 0, 0);

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Sinnvolle Fehlerbehandlung")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value as bool? ?? false;
            if (!boolValue || parameter == null)
            {
                return new SolidColorBrush(TransparentColor); 
            }

            try
            {
                return new SolidColorBrush((Color)parameter);
            }
            catch
            {
                return new SolidColorBrush(TransparentColor);
            }
        }

        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
