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
    public class ListJoinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            List<string> vs = new List<string>();
            if (value is IEnumerable lis)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in lis)
                    vs.Add(item.ToString());
            }
            else if (value is Array array)
            {
                foreach (var item in array)
                    vs.Add(item.ToString());
            }

            if (parameter == null)
                return string.Join(",", vs);
            else
                return string.Join(parameter.ToString(), vs);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
