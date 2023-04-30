using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace QualityContacts.UI.Helper
{
    /// <summary>
    /// WPF-XAML converter to convert booleans to a XAML-Brush.<br/>
    /// </summary>
    public class BooleanToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean to a Brush.
        /// </summary>
        /// <returns><see cref="Colors.Red"/>, if <see langword="true"/>, otherwise <see cref="Colors.LightGray"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue)
            {
                return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
