﻿using Ping9719.WpfEx.Mvvm;
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
    /// 伺服运动位置类型
    /// </summary>
    public enum ServoMoveType
    {
        /// <summary>
        /// 运动到指定位置
        /// </summary>
        MoveTo,
        /// <summary>
        /// 开始点动增加
        /// </summary>
        StartDotAdd,
        /// <summary>
        /// 开始点动增加
        /// </summary>
        EndDotAdd,
        /// <summary>
        /// 开始点动减少
        /// </summary>
        StartDotSub,
        /// <summary>
        /// 开始点动减少
        /// </summary>
        EndDotSub,
    }

    /// <summary>
    /// 工业控件：伺服
    /// </summary>
    public partial class IotServo : UserControl
    {
        /// <summary>
        /// 工业控件：伺服
        /// </summary>
        public IotServo()
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
            DependencyProperty.Register("Text", typeof(string), typeof(IotServo), new PropertyMetadata(null));


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
            DependencyProperty.Register("Location", typeof(double), typeof(IotServo), new PropertyMetadata((double)0));


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
            DependencyProperty.Register("IsFold", typeof(bool), typeof(IotServo), new PropertyMetadata(true));


        #endregion

        #region ModelSpeed

        //public int HomeModelSpeedIndex { get; set; } = 0;

        ///// <summary>
        ///// 取指定索引处的元素
        ///// </summary>
        ///// <param name="index">索引</param>
        ///// <returns>元素</returns>
        //public ServoModelSpeed this[int index] { get => ModelSpeeds.Skip(index).FirstOrDefault(); }
        ///// <summary>
        ///// 取指定名称的元素
        ///// </summary>
        ///// <param name="name">名称</param>
        ///// <returns>元素</returns>
        //public ServoModelSpeed this[string name] { get => ModelSpeeds.FirstOrDefault(o => o.Name == name); }

        /// <summary>
        /// 主页显示的模式速度
        /// </summary>
        public ServoModelSpeed ModelSpeedHome
        {
            get { return (ServoModelSpeed)GetValue(ModelSpeedHomeProperty); }
            set { SetValue(ModelSpeedHomeProperty, value); }
        }

        public static readonly DependencyProperty ModelSpeedHomeProperty =
            DependencyProperty.Register("ModelSpeedHome", typeof(ServoModelSpeed), typeof(IotServo), new PropertyMetadata(null));

        /// <summary>
        /// 全部的模式速度
        /// </summary>
        public List<ServoModelSpeed> ModelSpeeds
        {
            get { return (List<ServoModelSpeed>)GetValue(ModelSpeedsProperty); }
            set { SetValue(ModelSpeedsProperty, value); }
        }

        public static readonly DependencyProperty ModelSpeedsProperty =
            DependencyProperty.Register("ModelSpeeds", typeof(List<ServoModelSpeed>), typeof(IotServo), new PropertyMetadata(new List<ServoModelSpeed>(), new PropertyChangedCallback(ModelSpeedsChan)));

        private static void ModelSpeedsChan(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IotServo iotServo = (IotServo)d;
            if (e.NewValue != null && e.NewValue is List<ServoModelSpeed> ed)
            {
                iotServo.ModelSpeedHome = ed.FirstOrDefault();
            }
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
            DependencyProperty.Register("IsLoadIn", typeof(bool), typeof(IotServo), new PropertyMetadata(false));

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
            EventManager.RegisterRoutedEvent("LocationChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServo));

        private void jiad(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoadIn)
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoMoveType.StartDotAdd));
        }

        private void jiau(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoMoveType.EndDotAdd));
        }
        private void jiand(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoadIn)
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoMoveType.StartDotSub));
        }

        private void jianu(object sender, MouseButtonEventArgs e)
        {
            //if (!IsLoadIn)
            this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoMoveType.EndDotSub));
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
                if (!IsLoadIn)
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
            EventManager.RegisterRoutedEvent("SpeedChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServo));


        private void szsd(object sender, RoutedEventArgs e)
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
                if (!IsLoadIn)
                {
                    var servoModelSpeed = (ServoModelSpeed)d.DataContext;
                    servoModelSpeed.Speed = re;
                    this.RaiseEvent(new RoutedEventArgs(SpeedChangeEvent, servoModelSpeed));
                }
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

    /// <summary>
    /// 伺服模式速度
    /// </summary>
    public class ServoModelSpeed : BindableBase
    {
        private string name;
        /// <summary>
        /// 模式名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private int speed;
        /// <summary>
        /// 当前速度
        /// </summary>
        public int Speed
        {
            get { return speed; }
            set { SetProperty(ref speed, value); }
        }
    }
}
