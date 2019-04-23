using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Auditoria_V5
{
    public class UbicToUbic2Converter : IValueConverter

    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string a;
            a = (string)value;
            return a.Substring(0, 3) + "-" + a.Substring(3, 2) + "-" + a.Substring(5, 2) + "-" + a.Substring(7, 2) + "-" + a.Substring(9, 2);
        }

       public  object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
