using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 版本号与字符串转换器
    /// </summary>
    public class Version2StrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Version ver)
            {
                if (parameter != null && int.TryParse(parameter?.ToString(), out int intpar))
                    return ver.ToString(Math.Min(Math.Abs(intpar), 4));

                return ver.ToString();
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Version) && value != null)
                return new Version(value.ToString());

            return new Version();
        }
    }
}
