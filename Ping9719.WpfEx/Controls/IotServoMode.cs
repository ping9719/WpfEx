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
    /// 工业控件：伺服模式
    /// </summary>
    public class IotServoMode : Control
    {
        static IotServoMode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IotServoMode), new FrameworkPropertyMetadata(typeof(IotServoMode)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var ms = (Grid)this.Template.FindName("ms", this);
            var zd = (Grid)this.Template.FindName("zd", this);

            var butyd1 = (Button)this.Template.FindName("butyd1", this);
            var butyd2 = (Button)this.Template.FindName("butyd2", this);
            var butyd = (Button)this.Template.FindName("butyd", this);
            var textwz = (TextBox)this.Template.FindName("textwz", this);

            var text1 = (TextBox)this.Template.FindName("text1", this);
            var text2 = (TextBox)this.Template.FindName("text2", this);
            var butsd1 = (Button)this.Template.FindName("butsd1", this);
            var butsd2 = (Button)this.Template.FindName("butsd2", this);

            ms.PreviewMouseLeftButtonDown += (s, e) =>
            {
                IsVis1 = !IsVis1;
            };
            zd.PreviewMouseLeftButtonDown += (s, e) =>
            {
                IsFold = !IsFold;
            };

            butyd1.PreviewMouseLeftButtonDown += (s, e) =>
            {
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.StartDotAdd));
            };
            butyd1.PreviewMouseLeftButtonUp += (s, e) =>
            {
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.EndDotAdd));
            };
            butyd2.PreviewMouseLeftButtonDown += (s, e) =>
            {
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.StartDotSub));
            };
            butyd2.PreviewMouseLeftButtonUp += (s, e) =>
            {
                this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, ServoClickType.EndDotSub));
            };
            butyd.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textwz.Text))
                    textwz.Text = "无效的值";
                else if (double.TryParse(textwz.Text, out double re))
                    this.RaiseEvent(new RoutedEventArgs(LocationChangeEvent, re));
                else
                    textwz.Text = "错误格式";
            };

            butsd1.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(text1.Text))
                    text1.Text = "无效的值";
                else if (double.TryParse(text1.Text, out double re))
                    this.RaiseEvent(new RoutedEventArgs(SpeedChangeEvent, new ServoSpeed()
                    {
                        Name = "手动模式",
                        Speed = re,
                    }));
                else
                    text1.Text = "错误格式";
            };
            butsd2.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(text2.Text))
                    text2.Text = "无效的值";
                else if (double.TryParse(text2.Text, out double re))
                    this.RaiseEvent(new RoutedEventArgs(SpeedChangeEvent, new ServoSpeed()
                    {
                        Name = "自动模式",
                        Speed = re,
                    }));
                else
                    text2.Text = "错误格式";
            };
        }


        /// <summary>
        /// 名称
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IotServoMode), new PropertyMetadata("伺服1"));


        /// <summary>
        /// 当前位置
        /// </summary>
        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(string), typeof(IotServoMode), new PropertyMetadata("0"));


        /// <summary>
        /// 手动速度
        /// </summary>
        public string Speed1
        {
            get { return (string)GetValue(Speed1Property); }
            set { SetValue(Speed1Property, value); }
        }

        public static readonly DependencyProperty Speed1Property =
            DependencyProperty.Register("Speed1", typeof(string), typeof(IotServoMode), new PropertyMetadata("0"));


        /// <summary>
        /// 自动速度
        /// </summary>
        public string Speed2
        {
            get { return (string)GetValue(Speed2Property); }
            set { SetValue(Speed2Property, value); }
        }

        public static readonly DependencyProperty Speed2Property =
            DependencyProperty.Register("Speed2", typeof(string), typeof(IotServoMode), new PropertyMetadata("0"));


        /// <summary>
        /// 是否折叠
        /// </summary>
        public bool IsFold
        {
            get { return (bool)GetValue(IsFoldProperty); }
            set { SetValue(IsFoldProperty, value); }
        }

        public static readonly DependencyProperty IsFoldProperty =
            DependencyProperty.Register("IsFold", typeof(bool), typeof(IotServoMode), new PropertyMetadata(true));


        /// <summary>
        /// 是否显示手动速度
        /// </summary>
        public bool IsVis1
        {
            get { return (bool)GetValue(IsVis1Property); }
            set { SetValue(IsVis1Property, value); }
        }

        public static readonly DependencyProperty IsVis1Property =
            DependencyProperty.Register("IsVis1", typeof(bool), typeof(IotServoMode), new PropertyMetadata(true));


        /// <summary>
        /// 改变伺服的位置时
        /// </summary>
        public event RoutedEventHandler LocationChange
        {
            add { this.AddHandler(LocationChangeEvent, value); }
            remove { this.RemoveHandler(LocationChangeEvent, value); }
        }

        public static readonly RoutedEvent LocationChangeEvent =
            EventManager.RegisterRoutedEvent("LocationChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServoMode));

        /// <summary>
        /// 改变伺服的速度时
        /// </summary>
        public event RoutedEventHandler SpeedChange
        {
            add { this.AddHandler(SpeedChangeEvent, value); }
            remove { this.RemoveHandler(SpeedChangeEvent, value); }
        }

        public static readonly RoutedEvent SpeedChangeEvent =
            EventManager.RegisterRoutedEvent("SpeedChangeEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotServoMode));
    }
}
