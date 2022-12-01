using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 多行文本框
    /// </summary>
    public class TextBoxMulti : HandyControl.Controls.TextBox
    {
        /// <summary>
        /// 初始化多行文本框
        /// </summary>
        public TextBoxMulti() : base()
        {
            SetValue(TextWrappingProperty, System.Windows.TextWrapping.Wrap);
            SetValue(AcceptsReturnProperty, true);
            SetValue(VerticalContentAlignmentProperty, System.Windows.VerticalAlignment.Top);
            SetValue(VerticalScrollBarVisibilityProperty, System.Windows.Controls.ScrollBarVisibility.Visible);
            //HandyControl.Controls.InfoElement.SetPlaceholder(this, "没有信息");
        }
    }
}
