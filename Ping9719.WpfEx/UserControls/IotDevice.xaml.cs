using Ping9719.WpfEx.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 工业控件：设备
    /// </summary>
    public partial class IotDevice : UserControlBase
    {
        IEnumerable<DeviceStateData> stateDatas;
        IEnumerable<DeviceUrnData> urnDatas;
        IEnumerable<DeviceServo2Data> servoDatas;

        /// <summary>
        /// 工业控件：设备
        /// </summary>
        public IotDevice()
        {
            InitializeComponent();

            if (!this.IsInDesignMode)
            {
                LoadUi(new List<DeviceStateData>(), new List<DeviceUrnData>(), new List<DeviceServo2Data>());
            }
        }

        /// <summary>
        /// 加载多个Ui
        /// </summary>
        /// <param name="deviceDatass">暂支持 DeviceStateData，DeviceUrnData，DeviceServo2Data</param>
        public void LoadUi(params IEnumerable<IDeviceDataBase>[] deviceDatass)
        {
            foreach (var item in deviceDatass)
            {
                LoadUi(item);
            }
        }

        /// <summary>
        /// 加载Ui
        /// </summary>
        /// <param name="deviceDatas">暂支持 DeviceStateData，DeviceUrnData，DeviceServo2Data</param>
        public void LoadUi(IEnumerable<IDeviceDataBase> deviceDatas)
        {
            if (deviceDatas is IEnumerable<DeviceStateData> stateDatas)
            {
                this.stateDatas = stateDatas;
                if (stateDatas == null || !stateDatas.Any())
                {
                    boxcgq.Visibility = Visibility.Collapsed;
                    pancgq.Children.Clear();
                }
                else
                {
                    boxcgq.Visibility = Visibility.Visible;
                    pancgq.Children.Clear();

                    var moren = stateDatas.Where(o => string.IsNullOrWhiteSpace(o.GroupName));
                    var group = stateDatas.Where(o => !string.IsNullOrWhiteSpace(o.GroupName)).GroupBy(o => o.GroupName);

                    if (moren.Any())
                    {
                        WrapPanel wrapPanel = new WrapPanel();
                        foreach (var item in moren)
                        {
                            IotState iotState = new IotState();
                            iotState.DataContext = item;
                            iotState.SetBinding(IotState.TextProperty, nameof(item.Name));
                            iotState.SetBinding(IotState.IsOkProperty, nameof(item.IsOk));

                            wrapPanel.Children.Add(iotState);
                        }
                        pancgq.Children.Add(wrapPanel);
                    }
                    if (group.Any())
                    {
                        foreach (var item in group)
                        {
                            Expander expander = new Expander();
                            expander.Header = item.Key;

                            WrapPanel wrapPanel = new WrapPanel();
                            foreach (var item2 in item)
                            {
                                IotState iotState = new IotState();
                                iotState.DataContext = item2;
                                iotState.SetBinding(IotState.TextProperty, nameof(item2.Name));
                                iotState.SetBinding(IotState.IsOkProperty, nameof(item2.IsOk));

                                wrapPanel.Children.Add(iotState);
                            }
                            expander.Content = wrapPanel;
                            pancgq.Children.Add(expander);
                        }
                    }

                }
            }

            if (deviceDatas is IEnumerable<DeviceUrnData> urnDatas)
            {
                this.urnDatas = urnDatas;
                if (urnDatas == null || !urnDatas.Any())
                {
                    boxqg.Visibility = Visibility.Collapsed;
                    panqg.Children.Clear();
                }
                else
                {
                    boxqg.Visibility = Visibility.Visible;
                    panqg.Children.Clear();

                    var moren = urnDatas.Where(o => string.IsNullOrWhiteSpace(o.GroupName));
                    var group = urnDatas.Where(o => !string.IsNullOrWhiteSpace(o.GroupName)).GroupBy(o => o.GroupName);

                    if (moren.Any())
                    {
                        WrapPanel wrapPanel = new WrapPanel();
                        foreach (var item in moren)
                        {
                            IotUrn iotUrn = new IotUrn();
                            iotUrn.DataContext = item;
                            iotUrn.SetBinding(IotUrn.TextProperty, nameof(item.Name));
                            iotUrn.SetBinding(IotUrn.IsButBadge1Property, nameof(item.IsGoTo));
                            iotUrn.SetBinding(IotUrn.IsButBadge2Property, nameof(item.IsRetTo));
                            iotUrn.ButClick1 += IotUrn_ButClick1;
                            iotUrn.ButClick2 += IotUrn_ButClick2;

                            wrapPanel.Children.Add(iotUrn);
                        }
                        panqg.Children.Add(wrapPanel);
                    }
                    if (group.Any())
                    {
                        foreach (var item in group)
                        {
                            Expander expander = new Expander();
                            expander.Header = item.Key;

                            WrapPanel wrapPanel = new WrapPanel();
                            foreach (var item2 in item)
                            {
                                IotUrn iotUrn = new IotUrn();
                                iotUrn.DataContext = item2;
                                iotUrn.SetBinding(IotUrn.TextProperty, nameof(item2.Name));
                                iotUrn.SetBinding(IotUrn.IsButBadge1Property, nameof(item2.IsGoTo));
                                iotUrn.SetBinding(IotUrn.IsButBadge2Property, nameof(item2.IsRetTo));
                                iotUrn.ButClick1 += IotUrn_ButClick1;
                                iotUrn.ButClick2 += IotUrn_ButClick2;

                                wrapPanel.Children.Add(iotUrn);
                            }
                            expander.Content = wrapPanel;
                            panqg.Children.Add(expander);
                        }
                    }

                }

            }

            if (deviceDatas is IEnumerable<DeviceServo2Data> servoDatas)
            {
                this.servoDatas = servoDatas;
                if (servoDatas == null || !servoDatas.Any())
                {
                    boxsf.Visibility = Visibility.Collapsed;
                    pansf.Children.Clear();
                }
                else
                {
                    boxsf.Visibility = Visibility.Visible;
                    pansf.Children.Clear();

                    var moren = servoDatas.Where(o => string.IsNullOrWhiteSpace(o.GroupName));
                    var group = servoDatas.Where(o => !string.IsNullOrWhiteSpace(o.GroupName)).GroupBy(o => o.GroupName);

                    if (moren.Any())
                    {
                        WrapPanel wrapPanel = new WrapPanel();
                        foreach (var item in moren)
                        {
                            IotServo2 iotServo = new IotServo2();
                            iotServo.DataContext = item;
                            iotServo.SetBinding(IotServo2.TextProperty, nameof(item.Name));
                            iotServo.SetBinding(IotServo2.Speed1Property, nameof(item.JogSpeed));
                            iotServo.SetBinding(IotServo2.Speed2Property, nameof(item.AutoSpeed));
                            iotServo.SetBinding(IotServo2.LocationProperty, nameof(item.Location));
                            iotServo.SetBinding(IotServo2.IsVisSpeed1Property, new Binding(nameof(item.IsJog))
                            {
                                Mode = BindingMode.TwoWay,
                            });
                            iotServo.SetBinding(IotServo2.IsFoldProperty, new Binding(nameof(item.IsFold))
                            {
                                Mode = BindingMode.TwoWay,
                            });
                            iotServo.LocationChange += IotServo_LocationChange;
                            iotServo.SpeedChange += IotServo_SpeedChange;

                            wrapPanel.Children.Add(iotServo);
                        }
                        pansf.Children.Add(wrapPanel);
                    }
                    if (group.Any())
                    {
                        foreach (var item in group)
                        {
                            Expander expander = new Expander();
                            expander.Header = item.Key;

                            WrapPanel wrapPanel = new WrapPanel();
                            foreach (var item2 in item)
                            {
                                IotServo2 iotServo = new IotServo2();
                                iotServo.DataContext = item2;
                                iotServo.SetBinding(IotServo2.TextProperty, nameof(item2.Name));
                                iotServo.SetBinding(IotServo2.Speed1Property, nameof(item2.JogSpeed));
                                iotServo.SetBinding(IotServo2.Speed2Property, nameof(item2.AutoSpeed));
                                iotServo.SetBinding(IotServo2.LocationProperty, nameof(item2.Location));
                                iotServo.SetBinding(IotServo2.IsVisSpeed1Property, new Binding(nameof(item2.IsJog))
                                {
                                    Mode = BindingMode.TwoWay,
                                });
                                iotServo.SetBinding(IotServo2.IsFoldProperty, new Binding(nameof(item2.IsFold))
                                {
                                    Mode = BindingMode.TwoWay,
                                });
                                iotServo.LocationChange += IotServo_LocationChange;
                                iotServo.SpeedChange += IotServo_SpeedChange;

                                wrapPanel.Children.Add(iotServo);
                            }
                            expander.Content = wrapPanel;
                            pansf.Children.Add(expander);
                        }
                    }

                }
            }
        }

        #region 事件
        /// <summary>
        /// 气缸点击推或回。返回的OriginalSource参数为object[]，1为bool（true为推）；2为原绑定数据
        /// </summary>
        public event RoutedEventHandler UrnClick
        {
            add { this.AddHandler(UrnClickEvent, value); }
            remove { this.RemoveHandler(UrnClickEvent, value); }
        }

        public static readonly RoutedEvent UrnClickEvent =
            EventManager.RegisterRoutedEvent("UrnClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotDevice));

        private void IotUrn_ButClick1(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(UrnClickEvent, new object[] { true, ((FrameworkElement)sender).DataContext }));
        }

        private void IotUrn_ButClick2(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(UrnClickEvent, new object[] { false, ((FrameworkElement)sender).DataContext }));
        }

        /// <summary>
        /// 伺服点击的操作。返回的OriginalSource参数为object[]，1为枚举；2为新数据；3为原绑定数据
        /// </summary>
        public event RoutedEventHandler ServoClick
        {
            add { this.AddHandler(ServoClickEvent, value); }
            remove { this.RemoveHandler(ServoClickEvent, value); }
        }

        public static readonly RoutedEvent ServoClickEvent =
            EventManager.RegisterRoutedEvent("ServoClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IotDevice));


        private void IotServo_SpeedChange(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ServoClickEvent, new object[] { ServoClickType.SetSpeed, e.OriginalSource, ((FrameworkElement)sender).DataContext }));
        }

        private void IotServo_LocationChange(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ServoClickType)
                this.RaiseEvent(new RoutedEventArgs(ServoClickEvent, new object[] { e.OriginalSource, 0.0, ((FrameworkElement)sender).DataContext }));
            else
                this.RaiseEvent(new RoutedEventArgs(ServoClickEvent, new object[] { ServoClickType.MoveTo, e.OriginalSource, ((FrameworkElement)sender).DataContext }));
        }
        #endregion

        private void clisd(object sender, RoutedEventArgs e)
        {
            if (servoDatas == null || !servoDatas.Any())
                return;

            foreach (var item in servoDatas)
            {
                item.IsJog = true;
            }
        }

        private void clizd(object sender, RoutedEventArgs e)
        {
            if (servoDatas == null || !servoDatas.Any())
                return;

            foreach (var item in servoDatas)
            {
                item.IsJog = false;
            }
        }

        private void clifx(object sender, RoutedEventArgs e)
        {
            if (servoDatas == null || !servoDatas.Any())
                return;

            foreach (var item in servoDatas)
            {
                item.IsJog = !item.IsJog;
            }
        }

        private void clizka(object sender, RoutedEventArgs e)
        {
            if (servoDatas == null || !servoDatas.Any())
                return;

            foreach (var item in servoDatas)
            {
                item.IsFold = false;
            }
        }

        private void clizda(object sender, RoutedEventArgs e)
        {
            if (servoDatas == null || !servoDatas.Any())
                return;

            foreach (var item in servoDatas)
            {
                item.IsFold = true;
            }
        }
    }

    /// <summary>
    /// 设备数据基类
    /// </summary>
    public interface IDeviceDataBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 组名
        /// </summary>
        string GroupName { get; set; }
        /// <summary>
        /// 自定义数据
        /// </summary>
        object Tag { get; set; }
    }

    /// <summary>
    /// 设备数据
    /// </summary>
    public class DeviceStateData : BindableBase, IDeviceDataBase
    {
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        private bool isok;
        /// <summary>
        /// 是否ok状态
        /// </summary>
        public bool IsOk
        {
            get { return isok; }
            set { SetProperty(ref isok, value); }
        }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public object Tag { get; set; }
    }

    /// <summary>
    /// 设备气缸数据
    /// </summary>
    public class DeviceUrnData : BindableBase, IDeviceDataBase
    {
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }
        //public string GoAddress { get; set; }
        //public string RetAddress { get; set; }
        //public string GoToAddress { get; set; }
        //public string RetToAddress { get; set; }

        private bool isGoTo;
        /// <summary>
        /// 是否推到位
        /// </summary>
        public bool IsGoTo
        {
            get { return isGoTo; }
            set { SetProperty(ref isGoTo, value); }
        }

        private bool isRetTo;
        /// <summary>
        /// 是否回到位
        /// </summary>
        public bool IsRetTo
        {
            get { return isRetTo; }
            set { SetProperty(ref isRetTo, value); }
        }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public object Tag { get; set; }
    }

    /// <summary>
    /// 设备伺服数据
    /// </summary>
    public class DeviceServo2Data : BindableBase, IDeviceDataBase
    {
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        private int jogSpeed;
        /// <summary>
        /// 手动模式速度
        /// </summary>
        public int JogSpeed
        {
            get { return jogSpeed; }
            set { SetProperty(ref jogSpeed, value); }
        }

        private int autoSpeed;
        /// <summary>
        /// 自动模式速度
        /// </summary>
        public int AutoSpeed
        {
            get { return autoSpeed; }
            set { SetProperty(ref autoSpeed, value); }
        }

        private double location;
        /// <summary>
        /// 伺服当前位置
        /// </summary>
        public double Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        private bool isJog = true;
        /// <summary>
        /// 是否主页显示为手动模式
        /// </summary>
        public bool IsJog
        {
            get { return isJog; }
            set { SetProperty(ref isJog, value); }
        }

        private bool isFold = true;
        /// <summary>
        /// 是否折叠
        /// </summary>
        public bool IsFold
        {
            get { return isFold; }
            set { SetProperty(ref isFold, value); }
        }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public object Tag { get; set; }
    }
}
