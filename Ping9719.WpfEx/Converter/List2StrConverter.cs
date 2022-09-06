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
    /// 将数组、集合按照默认逗号分隔转为字符串
    /// </summary>
    [ValueConversion(typeof(IEnumerable), typeof(string))]
    [ValueConversion(typeof(Array), typeof(string))]
    public class List2StrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is IEnumerable lis)
            {
                if (parameter == null)
                    return string.Join(",", lis);
                else
                    return string.Join(parameter.ToString(), lis);
            }
            else if (value is Array array)
            {
                if (parameter == null)
                    return string.Join(",", array);
                else
                    return string.Join(parameter.ToString(), array);
            }
            else if (value is string[] strs)
            {
                if (parameter == null)
                    return string.Join(",", strs);
                else
                    return string.Join(parameter.ToString(), strs);
            }

            return "";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cf = parameter.ToString();
            if (cf == null)
                cf = ",";

            var sss = value.ToString().Split(new string[] { cf }, StringSplitOptions.None);

            if (targetType is IEnumerable lis)
            {
                //return sss.ToList();
            }
            else if (value is string[] strs)
            {
                return sss;
            }

            return null;
        }
    }
}
