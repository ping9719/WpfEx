using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 提示框
    /// </summary>
    public partial class MessageBoxTip : HandyControl.Controls.Window
    {
        internal MessageBoxTip()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 内容文本
        /// </summary>
        public string ContentText { get; private set; }
        /// <summary>
        /// 全部按钮
        /// </summary>
        public IEnumerable<string> Buttons { get; private set; }
        /// <summary>
        /// 确定后选中的按钮
        /// </summary>
        public string ClickButton { get; private set; }
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool IsVisCloseBut { get; private set; }

        /// <summary>
        /// 显示成功前,返回是否弹框
        /// </summary>
        public static Func<MessageBoxTip, bool> Showing;
        /// <summary>
        /// 显示成功后
        /// </summary>
        public static Action<MessageBoxTip> Showed;
        /// <summary>
        /// 点击成功后
        /// </summary>
        public static Action<MessageBoxTip> Clicked;

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string contentText, bool isVisCloseBut = true, Window owner = null, object tag = null) => Show(contentText, string.Empty, isVisCloseBut, owner, tag);

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="title">标题</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string contentText, string title, bool isVisCloseBut = true, Window owner = null, object tag = null) => Show(contentText, title, new string[] { "确认" }, isVisCloseBut, owner, tag);

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="buttons">按钮内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string contentText, IEnumerable<string> buttons, bool isVisCloseBut = true, Window owner = null, object tag = null) => Show(contentText, string.Empty, buttons, isVisCloseBut, owner, tag);

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">按钮内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string contentText, string title, IEnumerable<string> buttons, bool isVisCloseBut = true, Window owner = null, object tag = null)
        {
            string rInfo = null!;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTip();
                tipView.ContentText = contentText ?? "";
                tipView.Title = title ?? "";
                tipView.Buttons = buttons ?? new string[] { };
                tipView.IsVisCloseBut = isVisCloseBut;
                tipView.Tag = tag;
                tipView.DataContext = tipView;
                tipView.spacingPanel.Children.Clear();

                foreach (var item in tipView.Buttons)
                {
                    Button button = new Button()
                    {
                        Content = item,
                    };
                    button.Click += (s, e) =>
                    {
                        rInfo = ((Button)s).Content.ToString();
                        tipView.ClickButton = rInfo;
                        Clicked?.Invoke(tipView);
                        tipView.Close();
                    };

                    tipView.spacingPanel.Children.Add(button);
                }

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.Topmost = ownerIsNull;
                if (Showing?.Invoke(tipView) ?? true)
                {
                    SystemSounds.Asterisk.Play();
                    Showed?.Invoke(tipView);
                    tipView.ShowDialog();
                }
                tipView = null;
            }));

            return rInfo;
        }

        #region 私有
        private void closeClick(object sender, RoutedEventArgs e) => this.Close();
        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        #endregion

    }
}
