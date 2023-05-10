using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 工业控件：监听
    /// </summary>
    public class IotListener : Control
    {
        static IotListener()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IotListener), new FrameworkPropertyMetadata(typeof(IotListener)));
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(IotListener), new PropertyMetadata("xxx"));


        /// <summary>
        /// 值，字符串
        /// </summary>
        internal string ValueStr
        {
            get { return (string)GetValue(ValueStrProperty); }
            set { SetValue(ValueStrProperty, value); }
        }

        internal static readonly DependencyProperty ValueStrProperty =
            DependencyProperty.Register("ValueStr", typeof(string), typeof(IotListener), new PropertyMetadata(null));


        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(IotListener), new PropertyMetadata(null, OnValueChanged));


        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (o is IotListener listener)
            {
                listener.ValueStr = e.NewValue?.ToString();
            }
        }
    }
}
