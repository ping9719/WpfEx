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
            this.IsVisibleChanged += TextBoxScanner_IsVisibleChanged; ;
            this.TextChanged += TextBoxScanner_TextChanged;
        }

        int textTop = 0;
        bool isFir = false;
        private void TextBoxScanner_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!isFir)
            {
                isFir = true;
                textTop = Text?.Length ?? 0;
            }

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
                });
            }
            catch (Exception)
            {

            }
        }

        DateTime dt = DateTime.Now;
        bool isRun = false;

        private void TextBoxScanner_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textCount_ = Text?.Length ?? 0;
            //删除文本中
            if (textCount_ < textTop)
            {
                textTop = Text?.Length ?? 0;
                return;
            }

            textTop = Text?.Length ?? 0;

            var isNull = string.IsNullOrEmpty(Text);
            if (isNull)
                return;

            dt = DateTime.Now;

            if (isRun)
            {
                return;
            }
            else
            {
                isRun = true;
                var IntervalTime_ = IntervalTime;
                Task.Run(() =>
                {
                    try
                    {
                        while (true)
                        {
                            Thread.Sleep(1);

                            //扫码完成
                            var dtn = DateTime.Now;
                            if ((dtn - dt).TotalMilliseconds >= IntervalTime_)
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    if (AutoClear == TextBoxScannerAutoClear.NextClear)
                                    {
                                        if (Text.StartsWith(ScannerText))
                                        {
                                            Text = Text.Substring(ScannerText.Length);
                                            SelectionStart = Text.Length;
                                        }
                                    }

                                    ScannerText = Text;
                                    this.RaiseEvent(new RoutedEventArgs(TextScannerChangedEvent));

                                    if (AutoClear == TextBoxScannerAutoClear.Clear)
                                        Text = string.Empty;
                                });
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        isRun = false;
                    }
                });
            }
        }
    }
}
