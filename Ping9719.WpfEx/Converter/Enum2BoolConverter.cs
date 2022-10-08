using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 将枚举转为布尔
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class Enum2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == parameter.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(targetType, parameter.ToString());
        }
    }
}
