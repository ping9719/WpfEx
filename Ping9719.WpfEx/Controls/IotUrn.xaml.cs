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
    /// 工业控件：气缸
    /// </summary>
    public partial class IotUrn : UserControl
    {
        /// <summary>
        /// 工业控件：气缸
        /// </summary>
        public IotUrn()
        {
            InitializeComponent();
        }

        #region text
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IotUrn), new PropertyMetadata("XXX"));
        #endregion

        #region but1

        /// <summary>
        /// 是否显示按钮1上面的标记
        /// </summary>
        public bool IsButBadge1
        {
            get { return (bool)GetValue(IsButBadge1Property); }
            set { SetValue(IsButBadge1Property, value); }
        }

        public static readonly DependencyProperty IsButBadge1Property =
            DependencyProperty.Register("IsButBadge1", typeof(bool), typeof(IotUrn), new PropertyMetadata(false));

        /// <summary>
        /// 单击按钮1
        /// </summary>
        public event RoutedEventHandler ButClick1
        {
            add { this.AddHandler(ButClick1Event, value); }
            remove { this.RemoveHandler(ButClick1Event, value); }
        }

        public static readonly RoutedEvent ButClick1Event =
            EventManager.RegisterRoutedEvent("ButClick1Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        /// <summary>
        /// 单击按钮1按下
        /// </summary>
        public event RoutedEventHandler ButDownClick1
        {
            add { this.AddHandler(ButDownClick1Event, value); }
            remove { this.RemoveHandler(ButDownClick1Event, value); }
        }

        public static readonly RoutedEvent ButDownClick1Event =
            EventManager.RegisterRoutedEvent("ButDownClick1Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        /// <summary>
        /// 单击按钮1松开
        /// </summary>
        public event RoutedEventHandler ButUpClick1
        {
            add { this.AddHandler(ButUpClick1Event, value); }
            remove { this.RemoveHandler(ButUpClick1Event, value); }
        }

        public static readonly RoutedEvent ButUpClick1Event =
            EventManager.RegisterRoutedEvent("ButUpClick1Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButClick1Event));
        }
        private void Button1_DownClick(object sender, MouseButtonEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButDownClick1Event));
        }
        private void Button1_UpClick(object sender, MouseButtonEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButUpClick1Event));
        }
        #endregion

        #region but2

        /// <summary>
        /// 是否显示按钮2上面的标记
        /// </summary>
        public bool IsButBadge2
        {
            get { return (bool)GetValue(IsButBadge2Property); }
            set { SetValue(IsButBadge2Property, value); }
        }

        public static readonly DependencyProperty IsButBadge2Property =
            DependencyProperty.Register("IsButBadge2", typeof(bool), typeof(IotUrn), new PropertyMetadata(false));

        /// <summary>
        /// 单击按钮2
        /// </summary>
        public event RoutedEventHandler ButClick2
        {
            add { this.AddHandler(ButClick2Event, value); }
            remove { this.RemoveHandler(ButClick2Event, value); }
        }

        public static readonly RoutedEvent ButClick2Event =
            EventManager.RegisterRoutedEvent("ButClick2Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        /// <summary>
        /// 单击按钮2按下
        /// </summary>
        public event RoutedEventHandler ButDownClick2
        {
            add { this.AddHandler(ButDownClick2Event, value); }
            remove { this.RemoveHandler(ButDownClick2Event, value); }
        }

        public static readonly RoutedEvent ButDownClick2Event =
            EventManager.RegisterRoutedEvent("ButDownClick2Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        /// <summary>
        /// 单击按钮2松开
        /// </summary>
        public event RoutedEventHandler ButUpClick2
        {
            add { this.AddHandler(ButUpClick2Event, value); }
            remove { this.RemoveHandler(ButUpClick2Event, value); }
        }

        public static readonly RoutedEvent ButUpClick2Event =
            EventManager.RegisterRoutedEvent("ButUpClick2Event", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotUrn));

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButClick2Event));
        }
        private void Button2_DownClick(object sender, MouseButtonEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButDownClick2Event));
        }
        private void Button2_UpClick(object sender, MouseButtonEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ButUpClick2Event));
        }
        #endregion

    }
}
