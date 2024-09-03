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
        /// <summary>
        /// 任务最短停留时间，默认180ms（视觉暂留0.1，人眼辨认0.3）
        /// </summary>
        public static int TaskSleepTime = 180;
        Tuple<string, string, Action>[] Funcs;
        MainLoadWindowViewModel vModel = new MainLoadWindowViewModel();
        bool isErr = false;
        string errText = "";

        internal MainLoadWindow()
        {
            InitializeComponent();
            this.DataContext = vModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
            {
                foreach (var item in Funcs)
                {
                    vModel.Txt = item.Item1;
                    try
                    {
                        var dt1 = DateTime.Now;
                        item.Item3.Invoke();
                        var dt2 = (int)(DateTime.Now - dt1).TotalMilliseconds;

                        if (dt2 < TaskSleepTime)
                            await Task.Delay(TaskSleepTime - dt2);
                    }
                    catch (Exception ex)
                    {
                        isErr = true;
                        vModel.Txt = item.Item2;
                        vModel.IsClose = true;
                        vModel.IsVisLoad = false;
                        errText = ex.ToString();
                        break;
                    }
                }
            });

            if (!isErr)
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
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

            MainLoadWindow mainLoadWindow = new MainLoadWindow();
            mainLoadWindow.Funcs = funcs;
            mainLoadWindow.ShowDialog();
            return !mainLoadWindow.isErr;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(errText))
                MessageBox.Show(errText);
        }

        private void but_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void previewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
    }
}
