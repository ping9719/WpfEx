using HandyControl.Tools.Extension;
using Ping9719.WpfEx.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 入口页面加载等待窗体
    /// </summary>
    public partial class MainLoadWindow : HandyControl.Controls.Window
    {
        int runi = 0;
        MainLoadInfo[] Funcs;
        MainLoadWindowViewModel vModel = new MainLoadWindowViewModel();

        internal MainLoadWindow()
        {
            InitializeComponent();

            this.DataContext = vModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Run();
        }

        /// <summary>
        /// 显示并运行
        /// </summary>
        /// <param name="funcs">任务，1正在进行的任务的提示（如：正在进行XX中...），2此任务错误的提示（如：XX失败），3任务</param>
        /// <returns>是否全部成功</returns>
        public static bool Show(params Tuple<string, string, Action>[] funcs)
        {
            if (funcs == null || !funcs.Any())
                return true;

            return Show(funcs.Select(o => new MainLoadInfo(o.Item3, o.Item1, o.Item2)).ToArray());
        }

        /// <summary>
        /// 显示并运行
        /// </summary>
        /// <param name="funcs">加载的信息</param>
        /// <returns>是否全部成功</returns>
        public static bool Show(params MainLoadInfo[] funcs)
        {
            funcs = funcs?.Where(o => o.Task != null)?.ToArray();
            if (funcs == null || !funcs.Any())
                return true;

            MainLoadWindow mainLoadWindow = new MainLoadWindow();
            mainLoadWindow.Funcs = funcs;
            return mainLoadWindow.ShowDialog() ?? false;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var err = Funcs?.ElementAtOrDefault(runi)?.Err;
            if (err != null)
                MessageBox.Show(err.ToString());
        }

        private void but_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void cs(object sender, RoutedEventArgs e)
        {
            Run();
        }

        private void tg(object sender, RoutedEventArgs e)
        {
            runi++;
            Run();
        }

        private void Run()
        {
            Task.Run(() =>
            {
                vModel.Txt = "加载中...";
                vModel.IsClose = false;
                vModel.IsVisLoad = true;
                vModel.IsVisCs = false;
                vModel.IsVisTg = false;

                for (int i = 0; i < Funcs.Length; i++)
                {
                    if (runi > i)
                        continue;

                    var item = Funcs[i];

                    try
                    {
                        vModel.Txt = item.Info;

                        var dt1 = DateTime.Now;
                        item.Task.Invoke();
                        var dt2 = (int)(DateTime.Now - dt1).TotalMilliseconds;

                        if (dt2 < item.StayTime)
                            Thread.Sleep(item.StayTime - dt2);

                        runi++;
                    }
                    catch (Exception ex)
                    {
                        item.Err = ex;
                        vModel.Txt = item.InfoErr;
                        vModel.IsClose = true;
                        vModel.IsVisLoad = false;
                        vModel.IsVisCs = item.IsRetry;
                        vModel.IsVisTg = item.IsErrIgnore;
                        break;
                    }
                }

                if (runi >= Funcs.Length)
                    this.Dispatcher.Invoke(() =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    });
            });
        }
    }

    internal class MainLoadWindowViewModel : BindableBase
    {
        private string txt;
        public string Txt { get => txt; set { SetProperty(ref txt, value); } }

        private bool isVisLoad = true;
        public bool IsVisLoad { get => isVisLoad; set { SetProperty(ref isVisLoad, value); } }

        private bool isclode = false;
        public bool IsClose { get => isclode; set { SetProperty(ref isclode, value); } }

        private bool IsVisCs_ = false;
        public bool IsVisCs { get => IsVisCs_; set { SetProperty(ref IsVisCs_, value); } }

        private bool IsVisTg_ = false;
        public bool IsVisTg { get => IsVisTg_; set { SetProperty(ref IsVisTg_, value); } }
    }

    /// <summary>
    /// 窗体加载的信息
    /// </summary>
    public class MainLoadInfo
    {
        public MainLoadInfo() { }
        public MainLoadInfo(Action task, string info = "加载中...", string infoErr = "加载失败", bool isRetry = true, bool isErrIgnore = false, int stayTime = 180)
        {
            Task = task;
            Info = info;
            InfoErr = infoErr;
            IsRetry = isRetry;
            IsErrIgnore = isErrIgnore;
            StayTime = stayTime;
        }

        /// <summary>
        /// 加载的任务
        /// </summary>
        public Action Task { get; set; } = null;
        /// <summary>
        /// 提示。默认，加载中...
        /// </summary>
        public string Info { get; set; } = "加载中...";
        /// <summary>
        /// 加载失败显示的提示。默认，加载失败
        /// </summary>
        public string InfoErr { get; set; } = "加载失败";
        /// <summary>
        /// 加载中出现的错误
        /// </summary>
        internal Exception Err { get; set; } = null;
        /// <summary>
        /// 任务失败是否可以点击重试按钮，默认true
        /// </summary>
        public bool IsRetry { get; set; } = true;
        /// <summary>
        /// 是否可以对错误进行忽略并点击继续后继续，默认false
        /// </summary>
        public bool IsErrIgnore { get; set; } = false;
        /// <summary>
        /// 驻留视觉时间，默认180ms（视觉暂留0.1，人眼辨认0.3）
        /// </summary>
        public int StayTime { get; set; } = 180;
    }

}
