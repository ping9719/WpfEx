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
    /// 将枚举转为字符串（支持Description特性）
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class Enum2StrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is Enum enu)
            {
                var des = EnumHelp.GetAttributeDescription(enu);
                return des == null ? enu.ToString() : des;
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
