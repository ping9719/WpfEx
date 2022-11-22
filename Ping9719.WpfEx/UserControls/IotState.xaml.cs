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
    /// 工业控件：传感器状态
    /// </summary>
    public partial class IotState : UserControl
    {
        /// <summary>
        /// 工业控件：传感器状态
        /// </summary>
        public IotState()
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
            DependencyProperty.Register("Text", typeof(string), typeof(IotState), new PropertyMetadata("XXX"));
        #endregion

        #region isok
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? IsOk
        {
            get { return (bool?)GetValue(IsOkProperty); }
            set { SetValue(IsOkProperty, value); }
        }

        public static readonly DependencyProperty IsOkProperty =
            DependencyProperty.Register("IsOk", typeof(bool?), typeof(IotState), new PropertyMetadata(false));
        #endregion

        #region ToggleButton_Click

        /// <summary>
        /// 单击图标时
        /// </summary>
        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickEvent, value); }
            remove { this.RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("ClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotState));

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoadIn)
                this.RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
        #endregion

        #region isload

        /// <summary>
        /// 是否在加载中
        /// </summary>
        public bool IsLoadIn
        {
            get { return (bool)GetValue(IsLoadInProperty); }
            set { SetValue(IsLoadInProperty, value); }
        }

        public static readonly DependencyProperty IsLoadInProperty =
            DependencyProperty.Register("IsLoadIn", typeof(bool), typeof(IotState), new PropertyMetadata(false));

        #endregion


    }
}
