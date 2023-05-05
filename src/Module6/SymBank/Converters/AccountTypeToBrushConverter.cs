using SymBank.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SymBank.Converters
{
    public class AccountTypeToBrushConverter : IValueConverter
    {
        private static Brush[] _brushes =
        {
            Brushes.Azure,
            Brushes.Beige,
            Brushes.Aquamarine
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is AccountType)) return Brushes.White; // or null
            var accountType = (AccountType)value;
            return _brushes[(int)accountType];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush)) return (AccountType)(-1);
            var brush = (SolidColorBrush)value;
            for (int index = 0; index < 3; index++)
                if (_brushes[index] == brush)
                    return (AccountType)index;
            return (AccountType)(-1);
        }
    }
}
