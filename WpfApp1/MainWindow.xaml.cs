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
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

            //this.DataContext = ViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        ViewModel.Str = Guid.NewGuid().ToString();


            //        ViewModel.Datas.Add(new Namas() { Name = Guid.NewGuid().ToString() });
            //        ViewModel.Datas = ViewModel.Datas.ToList();

            //        System.Threading.Thread.Sleep(2000);

            //        ViewModel.Datas.RemoveAt(0);
            //    }

            //});

            ser.ModelSpeeds = new List<ServoModelSpeed>()
            {
                new ServoModelSpeed (){ Name="阿斯达12213dd ssdas大",Speed=123},
                new ServoModelSpeed (){ Name="adas",Speed=-285616512},
                new ServoModelSpeed (){ Name="阿斯达12213dd ssdas大",Speed=123},
                new ServoModelSpeed (){ Name="adas",Speed=-285616512},
            };

            var aaa = ser.ModelSpeeds;
            Task.Run(()=>
            {
                while (true)
                {
                    aaa.First().Speed++;
                    System.Threading.Thread.Sleep(1000);
                }

            });
        }

        private void aaa(object sender, RoutedEventArgs e)
        {

        
        }

        private void IotUrn_ButClick1(object sender, RoutedEventArgs e)
        {

        }

        private void IotUrn_ButClick2(object sender, RoutedEventArgs e)
        {

        }

        private void loa(object sender, RoutedEventArgs e)
        {
            ser.Text = e.OriginalSource.ToString();
        }

        private void spe(object sender, RoutedEventArgs e)
        {

        }
    }

    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            //Datas.CollectionChanged += (s, e) =>
            //{
            //    OnPropertyChanged("Datas");
            //};
        }

        private string str;

        public string Str
        {
            get { return str; }
            set { SetProperty(ref str, value); }
        }

        private List<Namas> datas = new List<Namas>();

        public List<Namas> Datas
        {
            get { return datas; }
            set { SetProperty(ref datas, value); }
        }

        public ICommand UpCommand { get => new DelegateCommand<string>(Up); }

        public void Up(string str)
        {
            Str = Guid.NewGuid().ToString() + str;
        }

        ///     public ICommand MyCommand { get; }
        ///     //构造函数
        ///     public MyClass()
        ///     {
        ///         MyCommand = new DelegateCommand(MyFun);
        ///     }
        ///     //执行方法
        ///     public void MyFun()
        ///     {
        ///         //code
        ///     }
    }

    public class Namas : BindableBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

    }
}
