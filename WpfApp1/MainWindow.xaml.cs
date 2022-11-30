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

            this.DataContext = ViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DeviceStateData> deviceStateDatas = new List<DeviceStateData>()
            {
                new DeviceStateData (){ Name="传感器1",GroupName="",IsOk=false},
                new DeviceStateData (){ Name="传感器2",GroupName="",IsOk=false},
                new DeviceStateData (){ Name="传感器3",GroupName="",IsOk=false},
                new DeviceStateData (){ Name="传感器4",GroupName="1",IsOk=false},
                new DeviceStateData (){ Name="传感器5",GroupName="1",IsOk=false},
                new DeviceStateData (){ Name="传感器6",GroupName="1",IsOk=false},
                new DeviceStateData (){ Name="传感器7",GroupName="1",IsOk=false},
                new DeviceStateData (){ Name="传感器8",GroupName="2",IsOk=false},
                new DeviceStateData (){ Name="传感器9",GroupName="2",IsOk=false},
            };
            List<DeviceUrnData> deviceUrnDatas = new List<DeviceUrnData>()
            {
                new DeviceUrnData (){Name="气缸1",GroupName="" },
                new DeviceUrnData (){Name="气缸2",GroupName="" },
                new DeviceUrnData (){Name="气缸3",GroupName="" },
                new DeviceUrnData (){Name="气缸4",GroupName="组1" },
                new DeviceUrnData (){Name="气缸5",GroupName="组1" },
                new DeviceUrnData (){Name="气缸6",GroupName="组1" },
                new DeviceUrnData (){Name="气缸7",GroupName="组1" },
                new DeviceUrnData (){Name="气缸8",GroupName="组2" },
                new DeviceUrnData (){Name="气缸9",GroupName="组2" },
            };
            List<DeviceServoData> deviceServoDatas = new List<DeviceServoData>()
            {
                new DeviceServoData (){Name="伺服1",GroupName="" },
                new DeviceServoData (){Name="伺服2",GroupName="" },
                new DeviceServoData (){Name="伺服3",GroupName="" },
                new DeviceServoData (){Name="伺服4",GroupName="组1" },
                new DeviceServoData (){Name="伺服5",GroupName="组1" },
                new DeviceServoData (){Name="伺服6",GroupName="组1" },
                new DeviceServoData (){Name="伺服7",GroupName="组1" },
                new DeviceServoData (){Name="伺服8",GroupName="组2" },
                new DeviceServoData (){Name="伺服9",GroupName="组2" },
            };
            dev.LoadUi(deviceStateDatas, deviceUrnDatas, deviceServoDatas);

            Task.Run(() =>
            {
                while (true)
                {
                    deviceStateDatas[2].IsOk = !deviceStateDatas[2].IsOk;
                    deviceStateDatas[4].IsOk = !deviceStateDatas[4].IsOk;

                    deviceUrnDatas[2].IsGoTo = !deviceUrnDatas[2].IsGoTo;
                    deviceUrnDatas[4].IsRetTo = !deviceUrnDatas[4].IsRetTo;

                    deviceServoDatas[2].AutoSpeed++;
                    deviceServoDatas[4].JogSpeed++;
                    deviceServoDatas[6].Location += 0.1;
                    System.Threading.Thread.Sleep(2000);
                }
            });
        }

        private void clike(object sender, RoutedEventArgs e)
        {
            var aaa = (object[])e.OriginalSource;
        }

        private void clikeser(object sender, RoutedEventArgs e)
        {

        }
    }

    public class MainWindowViewModel : BindableBase
    {

    }

}
