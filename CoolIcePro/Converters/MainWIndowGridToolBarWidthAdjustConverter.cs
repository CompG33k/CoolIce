using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoolIcePro.Converters
{
    public class MainWIndowGridToolBarWidthAdjustConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Do the conversion from bool to visibility
            //if (FirstColumn.Width.Value == 135)
            //{
            //    GridLength g = new GridLength(38);
            //    FirstColumn.Width = g;
            //}
            //else
            //{
            //    GridLength g = new GridLength(135);
            //    FirstColumn.Width = g;
            //}
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Do the conversion from visibility to bool
            return null;
        }
    }
}
