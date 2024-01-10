using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 日志文本框
    /// </summary>
    public class TextBoxLog : TextBox
    {
        /// <summary>
        /// 是否第一次加载
        /// </summary>
        public bool IsLoadedFirst { get; private set; } = false;
        private double HeightSum = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        public TextBoxLog() : base()
        {
            this.IsReadOnly = true;
            TextBoxLogAll.Add(this);

            if (this != null)
            {
                //大小改变置底
                this.SizeChanged += (sender, e) =>
                {
                    if (sender is TextBox textBox)
                    {
                        double oldHeight = e.PreviousSize.Height;
                        double newHeight = textBox.ActualHeight;
                        if (oldHeight > newHeight)
                        {
                            HeightSum += oldHeight - newHeight;
                            if (HeightSum > 10)
                            {
                                HeightSum = 0;
                                ScrollToEnd2();
                            }
                        }
                    }
                };

                //文本改变置底
                this.TextChanged += (_, _) =>
                {
                    ScrollToEnd2();
                };

                //关闭窗体释放
                this.Loaded += (_, _) =>
                {
                    if (!IsLoadedFirst)
                    {
                        try
                        {
                            Window parentWindow = Window.GetWindow(this);
                            if (parentWindow != null)
                                parentWindow.Closed += (_, _) =>
                                {
                                    TextBoxLogAll.Remove(this);
                                };
                        }
                        catch (Exception)
                        {

                        }
                    }
                    IsLoadedFirst = true;
                };
            }
        }

        /// <summary>
        /// 格式时间字符串。默认[HH:mm:ss.fff]
        /// </summary>
        public string TimeFormat
        {
            get { return (string)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        public static readonly DependencyProperty TimeFormatProperty =
            DependencyProperty.Register("TimeFormat", typeof(string), typeof(TextBoxLog), new PropertyMetadata("[HH:mm:ss.fff]"));

        /// <summary>
        /// 清除时保留的最大行的数量（数量的1.5倍触发截断）。默认600
        /// </summary>
        public int MaxLineNum
        {
            get { return (int)GetValue(MaxLineNumProperty); }
            set { SetValue(MaxLineNumProperty, value); }
        }

        public static readonly DependencyProperty MaxLineNumProperty =
            DependencyProperty.Register("MaxLineNum", typeof(int), typeof(TextBoxLog), new PropertyMetadata(600));

        /// <summary>
        /// 满足了清除条件时，是否清空所有文本。默认false
        /// </summary>
        public bool MaxLineNumClearAll
        {
            get { return (bool)GetValue(MaxLineNumClearAllProperty); }
            set { SetValue(MaxLineNumClearAllProperty, value); }
        }

        public static readonly DependencyProperty MaxLineNumClearAllProperty =
            DependencyProperty.Register("MaxLineNumClearAll", typeof(bool), typeof(TextBoxLog), new PropertyMetadata(false));


        /// <summary>
        /// 是否自动滚动。默认true
        /// </summary>
        public bool AutoScrollToEnd
        {
            get { return (bool)GetValue(AutoScrollToEndProperty); }
            set { SetValue(AutoScrollToEndProperty, value); }
        }

        public static readonly DependencyProperty AutoScrollToEndProperty =
            DependencyProperty.Register("AutoScrollToEnd", typeof(bool), typeof(TextBoxLog), new PropertyMetadata(true));


        /// <summary>
        /// 标识token
        /// </summary>
        public string TokenVal { get; private set; } = string.Empty;

        /// <summary>
        /// 标识token
        /// </summary>
        public string Token
        {
            get { return (string)GetValue(TokenProperty); }
            set { SetValue(TokenProperty, value); }
        }

        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.Register("Token", typeof(string), typeof(TextBoxLog), new PropertyMetadata("", OnTokenChanged));

        private static void OnTokenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (e.NewValue is string val && o is TextBoxLog tbl)
            {
                tbl.TokenVal = val;
            }
        }

        private void ScrollToEnd2()
        {
            if (AutoScrollToEnd)
            {
                var cha = LineCount - GetLastVisibleLineIndex();
                if (cha < 5)
                    ScrollToEnd();
            }
        }

        #region 静态
        /// <summary>
        /// 有新的日志加入
        /// </summary>
        public static Action<TextBoxLogInfo> TextBoxLogAdd;
        /// <summary>
        /// Token为空时查找第一个组件还是查找激活的组件，默认true
        /// </summary>
        public static bool TokenNullSeekFirst = true;
        /// <summary>
        /// 所有的正在使用的日志组件
        /// </summary>
        private static List<TextBoxLog> TextBoxLogAll = new List<TextBoxLog>();

        /// <summary>
        /// 追加日志信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="token"></param>
        public static void AddLog(string text, string token)
        {
            AddLog(text, token, null);
        }

        /// <summary>
        /// 追加日志信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <param name="token"></param>
        public static void AddLog(string text, string token = "", DateTime? time = null)
        {
            TextBoxLog tbl = GetTextBoxLog(token);
            TextBoxLogInfo info = new TextBoxLogInfo()
            {
                Time = time.HasValue ? time.Value : DateTime.Now,
                Text = text,
            };

            if (tbl == null)
            {
                TextBoxLogAdd?.BeginInvoke(info, null, null);
                return;
            }

            try
            {
                tbl?.Dispatcher?.Invoke(() =>
                {
                    info.Token = tbl.TokenVal;
                    info.TimeFormat = tbl.TimeFormat;
                    tbl.BeginChange();
                    tbl.AppendText($"{(string.IsNullOrEmpty(tbl.Text) ? "" : Environment.NewLine)}{info.ToString()}");

                    //清理
                    if (tbl.LineCount > tbl.MaxLineNum * 1.5)
                    {
                        if (tbl.MaxLineNumClearAll)
                        {
                            ClearLog(tbl);
                        }
                        else
                        {
                            try
                            {
                                var num = tbl.GetCharacterIndexFromLineIndex(tbl.MaxLineNum);
                                tbl.Text = tbl.Text.Substring(num);
                            }
                            catch (Exception ex)
                            {
                                ClearLog(tbl);

                                info.AddErr = ex;
                                TextBoxLogAdd?.BeginInvoke(info, null, null);
                                return;
                            }
                        }
                    }
                    tbl.EndChange();

                    TextBoxLogAdd?.BeginInvoke(info, null, null);
                });
            }
            catch (Exception ex)
            {
                info.AddErr = ex;
                TextBoxLogAdd?.BeginInvoke(info, null, null);
            }
        }

        /// <summary>
        /// 清除日志
        /// </summary>
        /// <param name="token"></param>
        public static void ClearLog(string token = "")
        {
            TextBoxLog tbl = GetTextBoxLog(token);
            ClearLog(tbl);
        }

        /// <summary>
        /// 清除日志
        /// </summary>
        /// <param name="tbl"></param>
        public static void ClearLog(TextBoxLog tbl)
        {
            try
            {
                tbl?.Dispatcher?.Invoke(() =>
                {
                    tbl.Clear();
                });
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 找寻组件
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TextBoxLog GetTextBoxLog(string token)
        {
            foreach (var item in TextBoxLogAll)
            {
                if (token == null)
                {
                    if (TokenNullSeekFirst)
                    {
                        return item;
                    }
                    else
                    {
                        if (item.IsVisible)
                            return item;
                    }
                }
                else if (item.TokenVal == token)
                {
                    return item;
                }
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    /// 日志加入信息
    /// </summary>
    public class TextBoxLogInfo
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 时间格式字符串
        /// </summary>
        public string TimeFormat { get; set; }
        /// <summary>
        /// 标识token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 添加到页面时的错误
        /// </summary>
        public Exception AddErr { get; set; }

        /// <summary>
        /// 转为字符串
        /// </summary>
        public override string ToString()
        {
            return $"{(string.IsNullOrWhiteSpace(TimeFormat) ? "" : Time.ToString(TimeFormat))}{Text}";
        }
    }
}
