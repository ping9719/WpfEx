using HandyControl.Controls;
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
    /// 工业控件：气缸模式
    /// </summary>
    public class IotUrnMode : Control
    {
        static IotUrnMode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IotUrnMode), new FrameworkPropertyMetadata(typeof(IotUrnMode)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            BorderElement.SetCornerRadius(Button1, new CornerRadius(3, 0, 0, 3));
            BorderElement.SetCornerRadius(Button2, new CornerRadius(0, 3, 3, 0));

            var grid1 = (Grid)this.Template.FindName("grid1", this);
            var grid2 = (Grid)this.Template.FindName("grid2", this);
            grid1.Children.Add(Button1);
            grid2.Children.Add(Button2);
        }

        /// <summary>
        /// 按钮1
        /// </summary>
        public Button Button1 { get; private set; } = new Button()
        {
            Content = "推",
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        /// <summary>
        /// 按钮2
        /// </summary>
        public Button Button2 { get; private set; } = new Button()
        {
            Content = "回",
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IotUrnMode), new PropertyMetadata("XXX"));

        /// <summary>
        /// 是否显示按钮1上面的标记
        /// </summary>
        public bool IsButBadge1
        {
            get { return (bool)GetValue(IsButBadge1Property); }
            set { SetValue(IsButBadge1Property, value); }
        }

        public static readonly DependencyProperty IsButBadge1Property =
            DependencyProperty.Register("IsButBadge1", typeof(bool), typeof(IotUrnMode), new PropertyMetadata(false));

        /// <summary>
        /// 是否显示按钮2上面的标记
        /// </summary>
        public bool IsButBadge2
        {
            get { return (bool)GetValue(IsButBadge2Property); }
            set { SetValue(IsButBadge2Property, value); }
        }

        public static readonly DependencyProperty IsButBadge2Property =
            DependencyProperty.Register("IsButBadge2", typeof(bool), typeof(IotUrnMode), new PropertyMetadata(false));

    }
}
