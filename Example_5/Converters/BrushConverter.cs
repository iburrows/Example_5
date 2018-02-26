using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Example_5.Converters
{
    class BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*var temp = (string)value;

            if (temp == "Green")
            {
                return "Orange";
            }

            return "Green";*/

            int temp = (int)value;

            if (temp == 1)
            {
                return "Green";
            }

            return "Orange";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
