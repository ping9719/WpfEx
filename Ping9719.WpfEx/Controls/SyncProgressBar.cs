using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 同步进度条
    /// </summary>
    public class SyncProgressBar : RangeBase
    {
        /// <summary>
        /// 同步错误点击时提示信息
        /// </summary>
        public string SyncErrClickInfo { get; set; }
        /// <summary>
        /// 在任务队列成功的时候显示控件。默认false
        /// </summary>
        public bool QueueTaskOkVisible { get; set; } = false;

        static SyncProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SyncProgressBar), new FrameworkPropertyMetadata(typeof(SyncProgressBar)));
        }

        private void QueueTask_StateChange(object sender, QueueTaskTimeState state, Exception exception)
        {
            if (state == QueueTaskTimeState.ForTask || state == QueueTaskTimeState.QueueTask)
            {
                this.Dispatcher.Invoke(() =>
                {
                    SyncState = SyncProgressBarState.SyncIn;
                });
            }
            else if (state == QueueTaskTimeState.EndTaskErr)
            {
                SyncErrClickInfo = exception?.ToString();

                this.Dispatcher.Invoke(() =>
                {
                    SyncState = SyncProgressBarState.SyncErr;
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    SyncState = QueueTaskOkVisible ? SyncProgressBarState.OkVisible : SyncProgressBarState.OkCollapsed;
                });
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var aa1 = this.Template.FindName("buttonErr", this);
            var aa2 = this.Template.FindName("textBlockErr", this);

            if (aa1 is Button but1)
                but1.Click += (sen1, obj1) =>
                {
                    ClickErr();
                };
            if (aa2 is TextBlock but2)
                but2.PreviewMouseLeftButtonDown += (sen1, obj1) =>
                {
                    ClickErr();
                };
        }

        void ClickErr()
        {
            if (!string.IsNullOrEmpty(SyncErrClickInfo))
            {
                HandyControl.Controls.MessageBox.Error(SyncErrClickInfo, SyncTextErr);
            }
        }

        /// <summary>
        /// 内部显示高度
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double InteriorHeight
        {
            get { return (double)GetValue(InteriorHeightProperty); }
            set { SetValue(InteriorHeightProperty, value); }
        }

        public static readonly DependencyProperty InteriorHeightProperty =
            DependencyProperty.Register("InteriorHeight", typeof(double), typeof(SyncProgressBar), new PropertyMetadata(12.0));


        /// <summary>
        /// 同步状态
        /// </summary>
        public SyncProgressBarState SyncState
        {
            get { return (SyncProgressBarState)GetValue(SyncStateProperty); }
            set { SetValue(SyncStateProperty, value); }
        }

        public static readonly DependencyProperty SyncStateProperty =
            DependencyProperty.Register("SyncState", typeof(SyncProgressBarState), typeof(SyncProgressBar), new PropertyMetadata(SyncProgressBarState.SyncIn));

        /// <summary>
        /// 同步成功描述文本
        /// </summary>
        public string SyncTextOk
        {
            get { return (string)GetValue(SyncTextOkProperty); }
            set { SetValue(SyncTextOkProperty, value); }
        }

        public static readonly DependencyProperty SyncTextOkProperty =
            DependencyProperty.Register("SyncTextOk", typeof(string), typeof(SyncProgressBar), new PropertyMetadata("同步完成"));


        /// <summary>
        /// 同步中描述文本
        /// </summary>
        public string SyncTextIn
        {
            get { return (string)GetValue(SyncTextInProperty); }
            set { SetValue(SyncTextInProperty, value); }
        }

        public static readonly DependencyProperty SyncTextInProperty =
            DependencyProperty.Register("SyncTextIn", typeof(string), typeof(SyncProgressBar), new PropertyMetadata("正在同步中..."));


        /// <summary>
        /// 同步错误描述文本
        /// </summary>
        public string SyncTextErr
        {
            get { return (string)GetValue(SyncTextErrProperty); }
            set { SetValue(SyncTextErrProperty, value); }
        }

        public static readonly DependencyProperty SyncTextErrProperty =
            DependencyProperty.Register("SyncTextErr", typeof(string), typeof(SyncProgressBar), new PropertyMetadata("同步失败"));

        #region QueueTask
        /// <summary>
        /// 任务队列
        /// </summary>
        public QueueTaskTime QueueTask
        {
            get { return (QueueTaskTime)GetValue(QueueTaskProperty); }
            set { SetValue(QueueTaskProperty, value); }
        }

        public static readonly DependencyProperty QueueTaskProperty =
            DependencyProperty.Register("QueueTask", typeof(QueueTaskTime), typeof(SyncProgressBar), new PropertyMetadata(null, OnTestChanged));

        private static void OnTestChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o == null || e == null)
                return;
            if (e.NewValue is QueueTaskTime queue && o is SyncProgressBar prog)
            {
                queue.StateChange += (object sender, QueueTaskTimeState state, Exception exception) =>
                {
                    if (state == QueueTaskTimeState.ForTask || state == QueueTaskTimeState.QueueTask)
                    {
                        prog.Dispatcher.Invoke(() =>
                        {
                            prog.SyncState = SyncProgressBarState.SyncIn;
                        });
                    }
                    else if (state == QueueTaskTimeState.EndTaskErr)
                    {
                        prog.Dispatcher.Invoke(() =>
                        {
                            prog.SyncErrClickInfo = exception?.ToString();
                            prog.SyncState = SyncProgressBarState.SyncErr;
                        });
                    }
                    else
                    {
                        prog.Dispatcher.Invoke(() =>
                        {
                            prog.SyncState = prog.QueueTaskOkVisible ? SyncProgressBarState.OkVisible : SyncProgressBarState.OkCollapsed;
                        });
                    }
                };
            }
        }
        #endregion
    }
}
