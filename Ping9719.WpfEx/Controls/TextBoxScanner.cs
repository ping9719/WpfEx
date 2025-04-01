using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 扫码文本框
    /// </summary>
    public class TextBoxScanner : TextBox
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public TextBoxScanner() : base()
        {
            this.IsVisibleChanged += TextBoxScanner_IsVisibleChanged;
            this.TextChanged += TextBoxScanner_TextChanged;
        }

        private void TextBoxScanner_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsAutoFocus && e.NewValue is true)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(400);
                    this.FocusInvoke();
                });
            }
        }


        /// <summary>
        /// 是否自动设置焦点（在显示控件的时候尝试将焦点设置为此元素）
        /// </summary>
        public bool IsAutoFocus
        {
            get { return (bool)GetValue(IsAutoFocusProperty); }
            set { SetValue(IsAutoFocusProperty, value); }
        }

        public static readonly DependencyProperty IsAutoFocusProperty =
            DependencyProperty.Register("IsAutoFocus", typeof(bool), typeof(TextBoxScanner), new PropertyMetadata(false));

        /// <summary>
        /// 自动清除文本（当达到计时变化间隔时）
        /// </summary>
        public TextBoxScannerAutoClear AutoClear
        {
            get { return (TextBoxScannerAutoClear)GetValue(AutoClearProperty); }
            set { SetValue(AutoClearProperty, value); }
        }

        public static readonly DependencyProperty AutoClearProperty =
            DependencyProperty.Register("AutoClear", typeof(TextBoxScannerAutoClear), typeof(TextBoxScanner), new PropertyMetadata(TextBoxScannerAutoClear.Clear));


        /// <summary>
        /// 触发方式，默认为时间
        /// </summary>
        public TextBoxScannerTriggerMode TriggerMode
        {
            get { return (TextBoxScannerTriggerMode)GetValue(TriggerModeProperty); }
            set { SetValue(TriggerModeProperty, value); }
        }

        public static readonly DependencyProperty TriggerModeProperty =
            DependencyProperty.Register("TriggerMode", typeof(TextBoxScannerTriggerMode), typeof(TextBoxScanner), new PropertyMetadata(TextBoxScannerTriggerMode.IntervalTime));

        /// <summary>
        /// 计时变化间隔，默认600ms
        /// </summary>
        public int IntervalTime
        {
            get { return (int)GetValue(IntervalTimeProperty); }
            set { SetValue(IntervalTimeProperty, value); }
        }

        public static readonly DependencyProperty IntervalTimeProperty =
            DependencyProperty.Register("IntervalTime", typeof(int), typeof(TextBoxScanner), new PropertyMetadata(600));

        /// <summary>
        /// 字符串结尾，默认（\n）
        /// </summary>
        public string StringEnd
        {
            get { return (string)GetValue(StringEndProperty); }
            set { SetValue(StringEndProperty, value); }
        }

        public static readonly DependencyProperty StringEndProperty =
            DependencyProperty.Register("StringEnd", typeof(string), typeof(TextBoxScanner), new PropertyMetadata("\n"));


        /// <summary>
        /// 文本扫码改变
        /// </summary>
        public event RoutedEventHandler TextScannerChanged
        {
            add { this.AddHandler(TextScannerChangedEvent, value); }
            remove { this.RemoveHandler(TextScannerChangedEvent, value); }
        }

        public static readonly RoutedEvent TextScannerChangedEvent =
            EventManager.RegisterRoutedEvent("TextScannerChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxScanner));

        /// <summary>
        /// 扫码文本
        /// </summary>
        public string ScannerText { get; set; } = string.Empty;

        /// <summary>
        /// 尝试将焦点设置为此元素，针对多线程优化
        /// </summary>
        public void FocusInvoke()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    base.Focus();
                    base.CaretIndex = Text.Length;
                });
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 清除所有内容
        /// </summary>
        public new void Clear()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    base.Clear();
                });
            }
            catch (Exception)
            {

            }
        }

        DateTime dt = DateTime.Now;
        string strTop = string.Empty;
        Task t1 = Task.FromResult(true);

        private void TextBoxScanner_TextChanged(object sender, TextChangedEventArgs e)
        {
            dt = DateTime.Now;

            if (e.Changes.Any(o => o.AddedLength > 0) && t1.IsCompleted)
            {
                var IntervalTime_ = IntervalTime;
                var StringEnd_ = StringEnd;
                var TriggerMode_ = TriggerMode;
                var sText = strTop;
                t1 = Task.Run(() =>
                {
                    try
                    {
                        while (true)
                        {
                            Thread.Sleep(1);

                            //扫码完成
                            if (TriggerMode_ == TextBoxScannerTriggerMode.IntervalTime && (DateTime.Now - dt).TotalMilliseconds >= IntervalTime_)
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    GoJx(sText);
                                });
                                break;
                            }
                            else if (TriggerMode_ == TextBoxScannerTriggerMode.StringEnd)
                            {
                                Thread.Sleep(100);
                                bool isend = false;
                                this.Dispatcher.Invoke(() =>
                                {
                                    isend = Text.EndsWith(StringEnd_);
                                    if (isend)
                                        GoJx(sText);
                                });

                                if (isend)
                                    break;
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }

            strTop = Text;

        }

        private void GoJx(string sText)
        {
            if (AutoClear == TextBoxScannerAutoClear.NoClear)
            {
                ScannerText = Text;

                if (!string.IsNullOrEmpty(ScannerText))
                    this.RaiseEvent(new RoutedEventArgs(TextScannerChangedEvent));

                base.CaretIndex = Text.Length;
            }
            else if (AutoClear == TextBoxScannerAutoClear.NextClear)
            {
                ScannerText = Text.StartsWith(sText) ? Text.Substring(sText.Length) : Text;

                if (!string.IsNullOrEmpty(ScannerText))
                    this.RaiseEvent(new RoutedEventArgs(TextScannerChangedEvent));

                Text = ScannerText;
                base.CaretIndex = Text.Length;
            }
            else if (AutoClear == TextBoxScannerAutoClear.Clear)
            {
                ScannerText = Text;

                if (!string.IsNullOrEmpty(ScannerText))
                    this.RaiseEvent(new RoutedEventArgs(TextScannerChangedEvent));

                Text = string.Empty;
            }
        }
    }

    /// <summary>
    /// 自动清空模式
    /// </summary>
    public enum TextBoxScannerAutoClear
    {
        /// <summary>
        /// 默认，不清空
        /// </summary>
        NoClear,
        /// <summary>
        /// 每次清空
        /// </summary>
        Clear,
        /// <summary>
        /// 下次清空
        /// </summary>
        NextClear
    }

    /// <summary>
    /// 自动清空模式
    /// </summary>
    public enum TextBoxScannerTriggerMode
    {
        /// <summary>
        /// 一定时间间隔没有变化
        /// </summary>
        IntervalTime,
        /// <summary>
        /// 指定的字符串结尾
        /// </summary>
        StringEnd,
    }
}
