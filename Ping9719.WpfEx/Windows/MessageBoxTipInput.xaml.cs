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

        /// <summary>
        /// 内容文本
        /// </summary>
        public string ContentText { get; private set; }
        /// <summary>
        /// 内容提示
        /// </summary>
        public string ContentHint { get; private set; }
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool IsVisCloseBut { get; private set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; private set; }
        /// <summary>
        /// 输入值
        /// </summary>
        public object InputValue { get; private set; }

        /// <summary>
        /// 显示成功前,返回是否弹框
        /// </summary>
        public static Func<MessageBoxTipInput, bool> Showing;
        /// <summary>
        /// 显示成功后
        /// </summary>
        public static Action<MessageBoxTipInput> Showed;
        /// <summary>
        /// 点击成功后
        /// </summary>
        public static Action<MessageBoxTipInput> Clicked;


        /// <summary>
        /// 显示输入提示框并尝试转为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="contentText">内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="defaultValue">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的内容（关闭为null）</returns>
        public static T? Show<T>(string contentText, Func<T, string> verify = null, string defaultValue = "", bool isVisCloseBut = true, Window owner = null, object tag = null) where T : struct => Show<T>(contentText, string.Empty, "请输入值", verify, defaultValue, isVisCloseBut, owner, tag);

        /// <summary>
        /// 显示输入提示框并尝试转为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="contentText">内容</param>
        /// <param name="title">标题</param>
        /// <param name="contentHint">内部内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="defaultValue">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的内容（关闭为null）</returns>
        public static T? Show<T>(string contentText, string title, string contentHint = "请输入值", Func<T, string> verify = null, string defaultValue = "", bool isVisCloseBut = true, Window owner = null, object tag = null) where T : struct
        {
            T? rval = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTipInput();
                tipView.ContentText = contentText;
                tipView.Title = title;
                tipView.ContentHint = contentHint;
                tipView.IsVisCloseBut = isVisCloseBut;
                tipView.DefaultValue = defaultValue;
                tipView.Tag = tag;
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
                    tipView.InputValue = val;
                    Clicked?.Invoke(tipView);
                    tipView.Close();
                };

                tipView.DataContext = tipView;

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.tb1.Text = defaultValue;
                tipView.Topmost = ownerIsNull;
                if (typeof(T) == typeof(bool))
                {
                    tipView.butt.Visibility = Visibility.Visible;
                    tipView.butf.Visibility = Visibility.Visible;
                }
                if (Showing?.Invoke(tipView) ?? true)
                {
                    SystemSounds.Asterisk.Play();
                    Showed?.Invoke(tipView);
                    tipView.tb1.Focus();
                    tipView.ShowDialog();
                }
                tipView = null;
            }));

            return rval;
        }

        /// <summary>
        /// 显示输入提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="defaultValue">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的文本（关闭为null）</returns>
        public static string Show(string contentText, Func<string, string> verify = null, string defaultValue = "", bool isVisCloseBut = true, bool isTrim = true, Window owner = null, object tag = null) => Show(contentText, string.Empty, "请输入值", verify, defaultValue, isVisCloseBut, isTrim, owner, tag);

        /// <summary>
        /// 显示输入提示框
        /// </summary>
        /// <param name="contentText">内容</param>
        /// <param name="title">标题</param>
        /// <param name="contentHint">内部内容</param>
        /// <param name="verify">验证委托</param>
        /// <param name="defaultValue">文本框里面的内容</param>
        /// <param name="isVisCloseBut">是否显示关闭按钮</param>
        /// <param name="owner">窗体所有者</param>
        /// <returns>输入的文本（关闭为null）</returns>
        public static string Show(string contentText, string title, string contentHint = "请输入值", Func<string, string> verify = null, string defaultValue = "", bool isVisCloseBut = true, bool isTrim = true, Window owner = null, object tag = null)
        {
            string? rval = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var tipView = new MessageBoxTipInput();
                tipView.ContentText = contentText;
                tipView.Title = title;
                tipView.ContentHint = contentHint;
                tipView.IsVisCloseBut = isVisCloseBut;
                tipView.DefaultValue = defaultValue;
                tipView.Tag = tag;
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
                    tipView.InputValue = wb;
                    Clicked?.Invoke(tipView);
                    tipView.Close();
                };
                tipView.DataContext = tipView;

                var ownerWindow = owner ?? WindowHelper.GetActiveWindow();
                var ownerIsNull = ownerWindow is null;

                tipView.Owner = ownerWindow;
                tipView.WindowStartupLocation = ownerIsNull ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                tipView.Topmost = ownerIsNull;
                tipView.tb1.Text = defaultValue;
                if (Showing?.Invoke(tipView) ?? true)
                {
                    SystemSounds.Asterisk.Play();
                    Showed?.Invoke(tipView);
                    tipView.tb1.Focus();
                    tipView.ShowDialog();
                }

                tipView = null;
            }));

            return rval!;
        }

        #region 私有
        private void TsTxtx(string text)
        {
            transit.Visibility = Visibility.Hidden;
            textBlock.Text = text;
            transit.Visibility = Visibility.Visible;
        }

        private void closeClick(object sender, RoutedEventArgs e) => this.Close();

        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Clickqk(object sender, RoutedEventArgs e) => tb1.Text = string.Empty;

        private void Button_Clickt(object sender, RoutedEventArgs e) => tb1.Text = bool.TrueString;

        private void Button_Clickf(object sender, RoutedEventArgs e) => tb1.Text = bool.FalseString;

        #endregion
    }
}
