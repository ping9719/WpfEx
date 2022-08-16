using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 对用户控件的扩展
    /// </summary>
    public class UserControlBase : UserControl
    {
        public UserControlBase()
        {
            IsVisibleChanged += UserControlEx_IsVisibleChanged;
        }

        private void UserControlEx_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (!IsLoadedFirst && e.NewValue is true)
            {
                if (LoadedFirst != null)
                    LoadedFirst(this, new EventArgs());

                IsLoadedFirst = true;
            }
        }

        #region 事件
        /// <summary>
        /// 首次加载并显示控件时发生
        /// </summary>
        public event EventHandler LoadedFirst;
        #endregion

        #region 属性
        /// <summary>
        /// 是否已第一次加载并显示
        /// </summary>
        public bool IsLoadedFirst { get; private set; }
        /// <summary>
        /// 是否处于设计模式
        /// </summary>
        public bool IsInDesignMode { get => DesignerProperties.GetIsInDesignMode(this); }
        #endregion

    }
}
