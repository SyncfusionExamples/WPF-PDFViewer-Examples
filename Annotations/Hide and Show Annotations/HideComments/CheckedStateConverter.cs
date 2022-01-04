using System;
using System.Globalization;
using System.Windows.Data;

namespace HideComments
{
    /// <summary>
    /// Notifies and converts the parent check box value changes to the child check boxes and vice versa.
    /// </summary>
    internal class CheckedStateConverter : IMultiValueConverter
    {
        //Converts the child check box values to the parent check box.
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)values[0] || !(bool)values[1] || !(bool)values[2])
                return false;
            return true;
        }

        //Converts the parent check box value to child check boxes.
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] values;
            if ((bool)value)
                values = new object[3] {true, true, true};
            else
                values = new object[3] { false, false, false };
            return values;
        }
    }
}
