using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
        private bool IsLoadedFirst = false;
        private double HeightSum = 0;
        private bool isColse = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public TextBoxLog() : base()
        {
            this.IsReadOnly = true;
            this.IsReadOnlyCaretVisible = true;
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

                //加载控件
                this.Loaded += (_, _) =>
                {
                    if (AutoScroll)
                        ScrollToEnd();

                    if (!IsLoadedFirst)
                    {
                        //所在窗体
                        try
                        {
                            Window parentWindow = Window.GetWindow(this);
                            if (parentWindow != null)
                                parentWindow.Closed += (_, _) =>
                                {
                                    isColse = true;
                                    base.Resources.Clear();
                                    TextBoxLogAll.Remove(this);
                                };
                        }
                        catch (Exception)
                        {

                        }

                        //刷新
                        Task.Run(async () =>
                        {
                            while (!isColse)
                            {
                                await Task.Delay(10);

                                var dt = DateTime.Now;
                                while (!queue.IsEmpty)
                                {
                                    if (stringb.Length > 0)
                                        stringb.Append(Environment.NewLine);

                                    if (queue.TryDequeue(out string a))
                                        stringb.Append(a);

                                    if (queue.IsEmpty || (DateTime.Now - dt).TotalMilliseconds > 20)
                                    {
                                        try
                                        {
                                            this?.Dispatcher?.Invoke(() =>
                                            {
                                                BeginChange();

                                                if (AddTextClearSelection)
                                                    SelectionLength = 0;
                                                var onestr = (string.IsNullOrEmpty(Text) ? string.Empty : Environment.NewLine);
                                                bool isScroll = AutoScroll && (LineCount - GetLastVisibleLineIndex()) < 5;
                                                stringb.Insert(0, onestr);
                                                AppendText(stringb.ToString());
                                                stringb.Clear();

                                                //清理
                                                if (LineCount >= MaxLineNumVal)
                                                {
                                                    if (MaxLineNumClearAll)
                                                    {
                                                        base.Clear();
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            var num = GetCharacterIndexFromLineIndex(MaxLineNum / 4);
                                                            if (num > 0 && num < Text.Length)
                                                                Text = Text.Substring(num);
                                                        }
                                                        catch (Exception)
                                                        {

                                                        }
                                                    }
                                                }

                                                if (isScroll)
                                                    ScrollToEnd();

                                                EndChange();
                                            }, DispatcherPriority.Background);
                                        }
                                        catch (Exception)
                                        {

                                        }
                                        finally
                                        {
                                            stringb.Clear();
                                        }
                                        break;
                                    }
                                }
                            }
                        });
                    }

                    IsLoadedFirst = true;
                };
            }
        }

        /// <summary>
        /// 格式时间字符串。
        /// </summary>
        public string TimeFormatVal { get; private set; } = "HH:mm:ss.fff=>";

        /// <summary>
        /// 格式时间字符串。默认 HH:mm:ss.fff=>
        /// </summary>
        public string TimeFormat
        {
            get { return (string)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        public static readonly DependencyProperty TimeFormatProperty =
            DependencyProperty.Register("TimeFormat", typeof(string), typeof(TextBoxLog), new PropertyMetadata("HH:mm:ss.fff=>", OnTimeFormatChanged));

        private static void OnTimeFormatChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (e.NewValue is string val && o is TextBoxLog tbl)
            {
                tbl.TimeFormatVal = val;
            }
        }

        /// <summary>
        /// 最大行数
        /// </summary>
        public int MaxLineNumVal { get; private set; } = 400;

        /// <summary>
        /// 最大行数（超过自动清除4分之1）。默认400
        /// </summary>
        public int MaxLineNum
        {
            get { return (int)GetValue(MaxLineNumProperty); }
            set { SetValue(MaxLineNumProperty, value); }
        }

        public static readonly DependencyProperty MaxLineNumProperty =
            DependencyProperty.Register("MaxLineNum", typeof(int), typeof(TextBoxLog), new PropertyMetadata(400, OnMaxLineNumChanged));

        private static void OnMaxLineNumChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (e.NewValue is int val && o is TextBoxLog tbl)
            {
                tbl.MaxLineNumVal = val;
            }
        }

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
        public bool AutoScroll
        {
            get { return (bool)GetValue(AutoScrollProperty); }
            set { SetValue(AutoScrollProperty, value); }
        }

        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.Register("AutoScroll", typeof(bool), typeof(TextBoxLog), new PropertyMetadata(true));


        /// <summary>
        /// 添加文本时，清空用户的选择。对性能有影响提升。（默认false）
        /// </summary>
        public bool AddTextClearSelection
        {
            get { return (bool)GetValue(AddTextClearSelectionProperty); }
            set { SetValue(AddTextClearSelectionProperty, value); }
        }

        public static readonly DependencyProperty AddTextClearSelectionProperty =
            DependencyProperty.Register("AddTextClearSelection", typeof(bool), typeof(TextBoxLog), new PropertyMetadata(false));


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
            DependencyProperty.Register("Token", typeof(string), typeof(TextBoxLog), new PropertyMetadata(string.Empty, OnTokenChanged));

        private static void OnTokenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (e.NewValue is string val && o is TextBoxLog tbl)
            {
                tbl.TokenVal = val;
            }
        }

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        StringBuilder stringb = new StringBuilder();
        /// <summary>
        /// 将字符串追加到文本
        /// </summary>
        /// <param name="text"></param>
        public void AppendText2(string text)
        {
            if (queue.Count > MaxLineNumVal)
                queue.TryDequeue(out string que);

            queue.Enqueue(text);
        }

        private void ScrollToEnd2()
        {
            if (AutoScroll && (LineCount - GetLastVisibleLineIndex()) < 5)
                ScrollToEnd();
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
        /// 组件数量
        /// </summary>
        public static int TextBoxLogCount { get => TextBoxLogAll.Count; }

        /// <summary>
        /// 追加日志信息
        /// </summary>
        public static void AddLog(string text, DateTime time)
        {
            AddLog(text, "", time, null);
        }

        /// <summary>
        /// 追加日志信息
        /// </summary>
        public static void AddLog(string text, string token = "", DateTime? time = null, object tag = null)
        {
            AddLogs(new string[] { text }, token, time, tag);
        }

        /// <summary>
        /// 追加多条日志信息
        /// </summary>
        public static void AddLogs(IEnumerable<string> texts, string token = "", DateTime? time = null, object tag = null)
        {
            TextBoxLog tbl = SeeControl(token);

            if (texts == null)
                return;

            var dt = time.HasValue ? time.Value : DateTime.Now;
            foreach (var text in texts)
            {
                TextBoxLogInfo info = new TextBoxLogInfo()
                {
                    Text = text,
                    Time = dt,
                    Tag = tag,
                    Token = token,
                    IsSeekToken = tbl != null,
                    SeekToken = tbl?.TokenVal,
                    TimeFormat = tbl?.TimeFormatVal,
                };

                tbl?.AppendText2(info.ToString());
                TextBoxLogAdd?.BeginInvoke(info, null, null);
            }
        }

        /// <summary>
        /// 清除所有信息
        /// </summary>
        /// <param name="token"></param>
        public static void ClearLog(string token = "")
        {
            TextBoxLog tbl = SeeControl(token);
            ClearLog(tbl);
        }

        /// <summary>
        /// 清除所有信息
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
        public static TextBoxLog SeeControl(string token)
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
        /// 文本信息
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 自定义的信息
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 标识token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 是否寻找到此Token关联的组件
        /// </summary>
        public bool IsSeekToken { get; set; }
        /// <summary>
        /// 找寻到的组件的token
        /// </summary>
        public string SeekToken { get; set; }
        /// <summary>
        /// 找寻到的组件的时间格式字符串
        /// </summary>
        public string TimeFormat { get; set; }

        /// <summary>
        /// 转为字符串
        /// </summary>
        public override string ToString()
        {
            return $"{(string.IsNullOrWhiteSpace(TimeFormat) ? "" : Time.ToString(TimeFormat))}{Text}";
        }
    }
}
