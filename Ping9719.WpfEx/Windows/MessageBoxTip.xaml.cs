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
        public MessageBoxTip()
        {
            InitializeComponent();
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string content, bool isVisCloseBut = true, Window owner = null)
        {
            return Show(content, string.Empty, isVisCloseBut);
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string content, string title, bool isVisCloseBut = true, Window owner = null)
        {
            return Show(content, title, new string[] { "确认" }, isVisCloseBut);
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="buttons">按钮内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string content, IEnumerable<string> buttons, bool isVisCloseBut = true, Window owner = null)
        {
            return Show(content, string.Empty, buttons, isVisCloseBut);
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">按钮内容</param>
        /// <param name="isVisCloseBut">界面右上角是否显示关闭按钮</param>
        /// <returns>点击的按钮文本</returns>
        public static string Show(string content, string title, IEnumerable<string> buttons, bool isVisCloseBut = true, Window owner = null)
        {
            string clikename = null!;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTip();
                tipView.DataContext = new { Title = title, Content = content, IsClose = isVisCloseBut };
                tipView.spacingPanel.Children.Clear();

                foreach (var item in buttons)
                {
                    Button button = new Button()
                    {
                        Content = item,
                    };
                    button.Click += (s, e) =>
                    {
                        clikename = ((Button)s).Content.ToString();
                        tipView.Close();
                    };

                    tipView.spacingPanel.Children.Add(button);
                }

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.Topmost = ownerIsNull;
                SystemSounds.Asterisk.Play();
                tipView.ShowDialog();
                tipView = null;
            }));

            return clikename;
        }

        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
