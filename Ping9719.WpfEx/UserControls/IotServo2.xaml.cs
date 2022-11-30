using Ping9719.WpfEx.Mvvm;
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
    /// 工业控件：伺服
    /// </summary>
    public partial class IotServo2 : UserControl
    {
        /// <summary>
        /// 工业控件：伺服
        /// </summary>
        public IotServo2()
        {
            InitializeComponent();
        }

        #region text

        /// <summary>
        /// 名称
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IotServo2), new PropertyMetadata(null));


        #endregion

        #region location

        /// <summary>
        /// 当前位置
        /// </summary>
        public double Location
        {
            get { return (double)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(double), typeof(IotServo2), new PropertyMetadata((double)0));


        #endregion

        #region Speed

        /// <summary>
        /// 速度1，默认手动速度
        /// </summary>
        public int Speed1
        {
            get { return (int)GetValue(Speed1Property); }
            set { SetValue(Speed1Property, value); }
        }

        public static readonly DependencyProperty Speed1Property =
            DependencyProperty.Register("Speed1", typeof(int), typeof(IotServo2), new PropertyMetadata(0));


        /// <summary>
        /// 速度2，默认自动速度
        /// </summary>
        public int Speed2
        {
            get { return (int)GetValue(Speed2Property); }
            set { SetValue(Speed2Property, value); }
        }

        public static readonly DependencyProperty Speed2Property =
            DependencyProperty.Register("Speed2", typeof(int), typeof(IotServo2), new PropertyMetadata(0));

        #endregion

        #region IsFold
        /// <summary>
        /// 是否折叠
        /// </summary>
        public bool IsFold
        {
            get { return (bool)GetValue(IsFoldProperty); }
            set { SetValue(IsFoldProperty, value); }
        }

        public static readonly DependencyProperty IsFoldProperty =
            DependencyProperty.Register("IsFold", typeof(bool), typeof(IotServo2), new PropertyMetadata(true));

        #endregion

        #region IsVisSpeed1

        /// <summary>
        /// 是否主页中显示速度1和模式，默认true
        /// </summary>
        public bool IsVisSpeed1
        {
            get { return (bool)GetValue(IsVisSpeed1Property); }
            set { SetValue(IsVisSpeed1Property, value); }
        }

        public static readonly DependencyProperty IsVisSpeed1Property =
            DependencyProperty.Register("IsVisSpeed1", typeof(bool), typeof(IotServo2), new PropertyMetadata(true));


        #endregion

        #region isload

        ///// <summary>
        ///// 是否在加载中
        ///// </summary>
        //public bool IsLoadIn
        //{
        //    get { return (bool)GetValue(IsLoadInProperty); }
        //    set { SetValue(IsLoadInProperty, value); }
        //}

        //public static readonly DependencyProperty IsLoadInProperty =
        //    DependencyProperty.Register("IsLoadIn", typeof(bool), typeof(IotServo2), new PropertyMetadata(false));

        #endregion

        #region 改变位置

        /// <summary>
        /// 尝试改变伺服的位置时
        /// </summary>
        public event RoutedEventHandler LocationChange
        {
            add { this.AddHandler(LocationChangeEvent, value); }
            remove { this.RemoveHandler(LocationChangeEvent, value); }
        }

        public static readonly RoutedEvent LocationChangeEvent =
            EventManager.RegisterRoutedEvent("LocationChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServo2));

        private void jiad(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.StartDotAdd));
        }

        private void jiau(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.EndDotAdd));
        }
        private void jiand(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.StartDotSub));
        }

        private void jianu(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.EndDotSub));
        }

        private void yd(object sender, RoutedEventArgs e)
        {
            var d = (FrameworkElement)sender;
            var aaa = (TextBox)d.Tag;
            if (string.IsNullOrWhiteSpace(aaa.Text))
            {
                aaa.Text = "无效的值";
                return;
            }
            if (double.TryParse(aaa.Text, out double re))
            {
                //aaa.IsError = false;
                //if (!IsLoadIn)
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, re));
            }
            else
            {
                //aaa.IsError = true;
                aaa.Text = "错误格式";
            }
        }
        #endregion

        #region 改变速度
        /// <summary>
        /// 尝试改变伺服的速度时
        /// </summary>
        public event RoutedEventHandler SpeedChange
        {
            add { this.AddHandler(SpeedChangeEvent, value); }
            remove { this.RemoveHandler(SpeedChangeEvent, value); }
        }

        public static readonly RoutedEvent SpeedChangeEvent =
            EventManager.RegisterRoutedEvent("SpeedChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServo2));

        private void szsd1(object sender, RoutedEventArgs e)
        {
            var d = (FrameworkElement)sender;
            var aaa = (TextBox)d.Tag;
            if (string.IsNullOrWhiteSpace(aaa.Text))
            {
                aaa.Text = "无效的值";
                return;
            }
            if (int.TryParse(aaa.Text, out int re))
            {
                //aaa.IsError = false;
                //if (!IsLoadIn)
                //{
                var servoSpeed = new ServoSpeed();
                servoSpeed.Name = "手动模式";
                servoSpeed.Speed = re;
                this.RaiseEvent(new RoutedEventArgs(SpeedChangeEvent, servoSpeed));
                //}
            }
            else
            {
                //aaa.IsError = true;
                aaa.Text = "错误格式";
            }
        }

        private void szsd2(object sender, RoutedEventArgs e)
        {
            var d = (FrameworkElement)sender;
            var aaa = (TextBox)d.Tag;
            if (string.IsNullOrWhiteSpace(aaa.Text))
            {
                aaa.Text = "无效的值";
                return;
            }
            if (int.TryParse(aaa.Text, out int re))
            {
                //aaa.IsError = false;
                //if (!IsLoadIn)
                //{
                var servoSpeed = new ServoSpeed();
                servoSpeed.Name = "自动模式";
                servoSpeed.Speed = re;
                this.RaiseEvent(new RoutedEventArgs(SpeedChangeEvent, servoSpeed));
                //}
            }
            else
            {
                //aaa.IsError = true;
                aaa.Text = "错误格式";
            }
        }
        #endregion

        private void qh(object sender, RoutedEventArgs e)
        {
            IsFold = !IsFold;
        }


    }
}
