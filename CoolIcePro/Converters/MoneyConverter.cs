using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoolIcePro.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                double d;
                string temp = value.ToString();
                if (double.TryParse(temp, out d))
                {
                    return String.Format("{0:C}", d);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if (value != null)
            {
                double d;
                string temp = value.ToString();
                if (double.TryParse(temp, out d))
                {
                    return String.Format("{0:C}", d);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
    }
}
