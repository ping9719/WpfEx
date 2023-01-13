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

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 入口页面加载等待窗体
    /// </summary>
    public partial class MainLoadWindow : HandyControl.Controls.Window
    {
        /// <summary>
        /// 任务最小间隔时间
        /// </summary>
        public static int TaskSleepTime = 500;
        Tuple<string, string, Action>[] Funcs;
        MainLoadWindowViewModel vModel = new MainLoadWindowViewModel();
        bool isUserClose = false;
        string err = "";

        internal MainLoadWindow()
        {
            InitializeComponent();
            this.DataContext = vModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                if (Funcs == null)
                    return;

                Stopwatch stopwatch = new Stopwatch();
                foreach (var item in Funcs)
                {
                    vModel.Txt = item.Item1;
                    try
                    {
                        stopwatch.Restart();
                        item.Item3.Invoke();
                        stopwatch.Stop();
                        if (stopwatch.ElapsedMilliseconds < TaskSleepTime)
                        {
                            Thread.Sleep(TaskSleepTime - Convert.ToInt32(stopwatch.ElapsedMilliseconds));
                        }
                    }
                    catch (Exception ex)
                    {
                        vModel.Txt = item.Item2;
                        vModel.IsClose = true;
                        err = ex.ToString();
                        stopwatch.Stop();
                        return;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
            });
        }

        /// <summary>
        /// 开始显示
        /// </summary>
        /// <param name="funcs">任务，1正在进行的任务的提示（如：正在进行XX中...），2此任务错误的提示（如：XX失败），3任务</param>
        /// <returns>是否全部成功</returns>
        public static bool Show(params Tuple<string, string, Action>[] funcs)
        {
            MainLoadWindow mainLoadWindow = new MainLoadWindow();
            mainLoadWindow.Funcs = funcs;
            mainLoadWindow.ShowDialog();
            return !mainLoadWindow.isUserClose;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(err))
                MessageBox.Show(err);
        }

        private void but_close(object sender, RoutedEventArgs e)
        {
            isUserClose = true;
            this.Close();
        }
    }

    internal class MainLoadWindowViewModel : BindableBase
    {
        private string txt;

        public string Txt { get => txt; set { SetProperty(ref txt, value); } }

        private bool isclode = false;

        public bool IsClose { get => isclode; set { SetProperty(ref isclode, value); } }
    }
}
