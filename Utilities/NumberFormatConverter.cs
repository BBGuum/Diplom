using System.Globalization;
using System.Windows.Data;

namespace TrueDiplom.Utilities
{
    public class NumberFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int number)
            {
                // Используем форматирование с пробелом как разделителем тысяч
                return string.Format(CultureInfo.InvariantCulture, "{0:N0}", number).Replace(',', ' ');
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
