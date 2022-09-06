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
    /// 将枚举转为数字
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class Enum2IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            if (value is Enum enu)
            {
                return (int)value;
            }

            return 0;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                if (value == null)
                    return Enum.ToObject(targetType, 0);
                else if (value is int intv)
                    return Enum.ToObject(targetType, intv);

                return Enum.ToObject(targetType, 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
