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
        /// <summary>
        /// 初始化
        /// </summary>
        public UserControlBase()
        {
            IsVisibleChanged += UserControlEx_IsVisibleChanged;
        }

        private void UserControlEx_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is true)
            {
                if (!IsLoadedVisible)
                {
                    if (LoadedVisibleFirst != null)
                        LoadedVisibleFirst(this, new EventArgs());
                }

                if (LoadedVisible != null)
                    LoadedVisible(this, new EventArgs());

                IsLoadedVisible = true;
            }
        }

        #region 事件
        /// <summary>
        /// 首次加载并显示控件时发生
        /// </summary>
        public event EventHandler LoadedVisibleFirst;

        /// <summary>
        /// 加载并显示控件时发生
        /// </summary>
        public event EventHandler LoadedVisible;
        #endregion

        #region 属性
        /// <summary>
        /// 是否已加载并显示界面
        /// </summary>
        public bool IsLoadedVisible { get; private set; }
        /// <summary>
        /// 是否处于设计模式
        /// </summary>
        public bool IsInDesignMode { get => DesignerProperties.GetIsInDesignMode(this); }
        #endregion

    }
}
