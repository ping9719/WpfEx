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
    /// 将枚举转为字符串
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is Enum enu)
            {
                return enu.ToString();
            }

            return "";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            try
            {
                return Enum.Parse(targetType, value.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
