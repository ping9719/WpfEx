using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    [ToolboxItem(true)]
    public class IotState : ButtonBase
    {
        static IotState()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IotState), new FrameworkPropertyMetadata(typeof(IotState)));
        }

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

        /// <summary>
        /// 成功状态下的颜色
        /// </summary>
        public Brush OkBrush
        {
            get { return (Brush)GetValue(OkBrushProperty); }
            set { SetValue(OkBrushProperty, value); }
        }

        public static readonly DependencyProperty OkBrushProperty =
            DependencyProperty.Register("OkBrush", typeof(Brush), typeof(IotState));


        /// <summary>
        /// 不成功状态下的颜色
        /// </summary>
        public Brush NotOkBrush
        {
            get { return (Brush)GetValue(NotOkBrushProperty); }
            set { SetValue(NotOkBrushProperty, value); }
        }

        public static readonly DependencyProperty NotOkBrushProperty =
            DependencyProperty.Register("NotOkBrush", typeof(Brush), typeof(IotState));

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
            DependencyProperty.Register("InteriorHeight", typeof(double), typeof(IotState));

    }
}
