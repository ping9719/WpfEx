using HandyControl.Controls;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Window = System.Windows.Window;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 提示框
    /// </summary>
    public partial class MessageBoxTipInput : HandyControl.Controls.Window
    {
        public MessageBoxTipInput()
        {
            InitializeComponent();
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 显示输入提示框并尝试转为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="content">内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="textContent">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的内容（关闭为null）</returns>
        public static T? Show<T>(string content, Func<T, string> verify = null, string textContent = "", bool isVisCloseBut = true, Window owner = null) where T : struct
        {
            return Show<T>(content, string.Empty, "请输入值", verify, textContent, isVisCloseBut, owner);
        }

        /// <summary>
        /// 显示输入提示框并尝试转为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="contentHint">内部内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="textContent">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的内容（关闭为null）</returns>
        public static T? Show<T>(string content, string title, string contentHint = "请输入值", Func<T, string> verify = null, string textContent = "", bool isVisCloseBut = true, Window owner = null) where T : struct
        {
            T? rval = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTipInput();
                tipView.tb1.KeyDown += (s, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        tipView.qr.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                };
                tipView.qr.Click += (sender, e) =>
                {
                    if (string.IsNullOrWhiteSpace(tipView.tb1.Text))
                    {
                        tipView.TsTxtx("请填写文本。");
                        return;
                    }

                    T val;
                    try
                    {
                        val = (T)Convert.ChangeType(tipView.tb1.Text, typeof(T));
                    }
                    catch (Exception)
                    {
                        tipView.TsTxtx($"无法将值{tipView.tb1.Text}换为{typeof(T).Name}。");
                        return;
                    }

                    var ts = verify?.Invoke(val);
                    if (!string.IsNullOrEmpty(ts))
                    {
                        tipView.TsTxtx(ts!);
                        return;
                    }
                    rval = val;
                    tipView.Close();
                };
                tipView.DataContext = new { Title = title, Content = content, ContentHint = contentHint, IsClose = isVisCloseBut };

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.tb1.Text = textContent;
                tipView.tb1.Focus();
                tipView.Topmost = ownerIsNull;
                SystemSounds.Asterisk.Play();
                if (typeof(T) == typeof(bool))
                {
                    tipView.butt.Visibility = Visibility.Visible;
                    tipView.butf.Visibility = Visibility.Visible;
                }
                tipView.ShowDialog();
                tipView = null;
            }));

            return rval;
        }

        /// <summary>
        /// 显示输入提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="textContent">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的文本（关闭为null）</returns>
        public static string Show(string content, Func<string, string> verify = null, string textContent = "", bool isVisCloseBut = true, bool isTrim = true, Window owner = null)
        {
            return Show(content, string.Empty, "请输入值", verify, textContent, isVisCloseBut, isTrim, owner);
        }

        /// <summary>
        /// 显示输入提示框
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="contentHint">内部内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="textContent">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的文本（关闭为null）</returns>
        public static string Show(string content, string title, string contentHint = "请输入值", Func<string, string> verify = null, string textContent = "", bool isVisCloseBut = true, bool isTrim = true, Window owner = null)
        {
            string? rval = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTipInput();
                tipView.tb1.KeyDown += (s, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        e.Handled = true;
                        tipView.qr.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                };
                tipView.qr.Click += (sender, e) =>
                {
                    var wb = isTrim ? tipView.tb1.Text.Trim() : tipView.tb1.Text;
                    if (string.IsNullOrEmpty(wb))
                    {
                        tipView.TsTxtx("请填写文本。");
                        return;
                    }

                    var ts = verify?.Invoke(wb);
                    if (!string.IsNullOrEmpty(ts))
                    {
                        tipView.TsTxtx(ts!);
                        return;
                    }
                    rval = wb;
                    tipView.Close();
                };
                tipView.DataContext = new { Title = title, Content = content, ContentHint = contentHint, IsClose = isVisCloseBut };

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.Topmost = ownerIsNull;
                tipView.tb1.Text = textContent;
                tipView.tb1.Focus();
                SystemSounds.Asterisk.Play();
                tipView.ShowDialog();
                tipView = null;
            }));

            return rval!;
        }

        private void TsTxtx(string text)
        {
            transit.Visibility = Visibility.Hidden;
            textBlock.Text = text;
            transit.Visibility = Visibility.Visible;
        }

        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Clickqk(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
        }

        private void Button_Clickt(object sender, RoutedEventArgs e)
        {
            tb1.Text = true.ToString();
        }

        private void Button_Clickf(object sender, RoutedEventArgs e)
        {
            tb1.Text = false.ToString();
        }
    }
}
