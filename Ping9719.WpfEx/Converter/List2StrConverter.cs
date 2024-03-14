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
    public class List2StrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is string)
            {
                return value.ToString();
            }
            else if (value is IEnumerable lis)
            {
                List<string> strList = new List<string>();
                string sep = parameter == null ? "," : parameter.ToString();

                foreach (var item in lis)
                {
                    strList.Add(item.ToString());
                }

                return string.Join(sep, strList);
            }

            return "";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
