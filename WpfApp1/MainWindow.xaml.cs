using HandyControl.Controls;
using Ping9719.WpfEx;
using Ping9719.WpfEx.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        MainWindowViewModel ViewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = ViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void textc(object sender, RoutedEventArgs e)
        {
            var aaa = (TextBoxScanner)sender;
            var a1 = aaa.Text;
            var a2 = aaa.ScannerText;

            Growl.Info(a1 + ";" + a2);
        }

        private void aaaaa(object sender, RoutedEventArgs e)
        {
            var aaa = (TextBoxScanner)sender;
            var a1 = aaa.Text;
            var a2 = aaa.ScannerText;

            Growl.Info(a1);
        }
    }

    public class MainWindowViewModel : BindableBase
    {
        public ICommand MyCommand => new DelegateCommand(My);
        //执行方法
        public void My()
        {
            //code
        }
    }

}
