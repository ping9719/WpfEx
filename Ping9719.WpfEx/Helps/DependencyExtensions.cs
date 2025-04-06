using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 依赖属性扩展
    /// </summary>
    public static class DependencyExtensions
    {
        /// <summary>
        /// 设置为默认值
        /// </summary>
        public static bool SetIfDefault<T>(this DependencyObject o, DependencyProperty property, T value)
        {
            if (DependencyPropertyHelper.GetValueSource(o, property).BaseValueSource == BaseValueSource.Default)
            {
                o.SetValue(property, value);

                return true;
            }

            return false;
        }
    }
}
