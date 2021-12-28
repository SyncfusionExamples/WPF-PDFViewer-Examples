using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HideComments
{
    internal class CheckedStateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)values[0] || !(bool)values[1] || !(bool)values[2])
                return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] values = null;
            if ((bool)value)
                values = new object[3] {true, true, true};
            else
                values = new object[3] { false, false, false };
            return values;
        }
    }
}
