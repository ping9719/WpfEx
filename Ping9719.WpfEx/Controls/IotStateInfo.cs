using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class IotStateInfo : Control
    {
        static IotStateInfo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IotStateInfo), new FrameworkPropertyMetadata(typeof(IotStateInfo)));
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
            DependencyProperty.Register("Header", typeof(string), typeof(IotStateInfo), new PropertyMetadata("xxx"));

        /// <summary>
        /// 后缀文本
        /// </summary>
        public string Postfix
        {
            get { return (string)GetValue(PostfixProperty); }
            set { SetValue(PostfixProperty, value); }
        }

        public static readonly DependencyProperty PostfixProperty =
            DependencyProperty.Register("Postfix", typeof(string), typeof(IotStateInfo), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 值，字符串
        /// </summary>
        internal string ValueStr
        {
            get { return (string)GetValue(ValueStrProperty); }
            set { SetValue(ValueStrProperty, value); }
        }

        internal static readonly DependencyProperty ValueStrProperty =
            DependencyProperty.Register("ValueStr", typeof(string), typeof(IotStateInfo), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 值，bool
        /// </summary>
        internal bool ValueBool
        {
            get { return (bool)GetValue(ValueBoolProperty); }
            set { SetValue(ValueBoolProperty, value); }
        }

        internal static readonly DependencyProperty ValueBoolProperty =
            DependencyProperty.Register("ValueBool", typeof(bool), typeof(IotStateInfo), new PropertyMetadata(false));

        /// <summary>
        /// 是否bool类型
        /// </summary>
        internal bool IsBool
        {
            get { return (bool)GetValue(IsBoolProperty); }
            set { SetValue(IsBoolProperty, value); }
        }

        internal static readonly DependencyProperty IsBoolProperty =
            DependencyProperty.Register("IsBool", typeof(bool), typeof(IotStateInfo), new PropertyMetadata(false));

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(IotStateInfo), new PropertyMetadata(null, OnValueChanged));

        /// <summary>
        /// 成功状态下的颜色
        /// </summary>
        public Brush OkBrush
        {
            get { return (Brush)GetValue(OkBrushProperty); }
            set { SetValue(OkBrushProperty, value); }
        }

        public static readonly DependencyProperty OkBrushProperty =
            DependencyProperty.Register("OkBrush", typeof(Brush), typeof(IotStateInfo));


        /// <summary>
        /// 不成功状态下的颜色
        /// </summary>
        public Brush NotOkBrush
        {
            get { return (Brush)GetValue(NotOkBrushProperty); }
            set { SetValue(NotOkBrushProperty, value); }
        }

        public static readonly DependencyProperty NotOkBrushProperty =
            DependencyProperty.Register("NotOkBrush", typeof(Brush), typeof(IotStateInfo));

        /// <summary>
        /// 内部高度
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double InteriorHeight
        {
            get { return (double)GetValue(InteriorHeightProperty); }
            set { SetValue(InteriorHeightProperty, value); }
        }

        public static readonly DependencyProperty InteriorHeightProperty =
            DependencyProperty.Register("InteriorHeight", typeof(double), typeof(IotStateInfo));

        /// <summary>
        /// 是否字符串bool也当做bool
        /// </summary>
        public bool ValueStrBoolIsBool
        {
            get { return (bool)GetValue(ValueStrBoolIsBoolProperty); }
            set { SetValue(ValueStrBoolIsBoolProperty, value); }
        }

        public static readonly DependencyProperty ValueStrBoolIsBoolProperty =
            DependencyProperty.Register("ValueStrBoolIsBool", typeof(bool), typeof(IotStateInfo), new PropertyMetadata(true));



        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (o is IotStateInfo listener)
            {
                if (e.NewValue == null)
                    return;
                else if (e.NewValue is bool bo)
                {
                    listener.IsBool = true;
                    listener.ValueBool = bo;
                    listener.ValueStr = string.Empty;
                }
                else
                {
                    var str = e.NewValue.ToString();
                    if (listener.ValueStrBoolIsBool && str.ToUpper() == "TRUE")
                    {
                        listener.IsBool = true;
                        listener.ValueBool = true;
                        listener.ValueStr = string.Empty;
                    }
                    else if (listener.ValueStrBoolIsBool && str.ToUpper() == "FALSE")
                    {
                        listener.IsBool = true;
                        listener.ValueBool = false;
                        listener.ValueStr = string.Empty;
                    }
                    else
                    {
                        listener.IsBool = false;
                        listener.ValueBool = false;
                        listener.ValueStr = e.NewValue.ToString();
                    }
                }
            }
        }
    }
}
