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
    /// IotPlc.xaml 的交互逻辑
    /// </summary>
    public partial class IotDevice : UserControl
    {
        public IotDevice()
        {
            InitializeComponent();
        }

        #region model

        ///// <summary>
        ///// 模式，key 名称 value 值
        ///// </summary>
        //public Dictionary<string, object> Model
        //{
        //    get { return (Dictionary<string, object>)GetValue(ModelProperty); }
        //    set { SetValue(ModelProperty, value); }
        //}

        //public static readonly DependencyProperty ModelProperty =
        //    DependencyProperty.Register("Model", typeof(Dictionary<string, object>), typeof(IotDevice), new PropertyMetadata(new Dictionary<string, object>()
        //    {
        //        { "手动模式",1},
        //        { "自动模式",2},
        //    }));


        ///// <summary>
        ///// 模式地址
        ///// </summary>
        //public string ModelAddress
        //{
        //    get { return (string)GetValue(ModelAddressProperty); }
        //    set { SetValue(ModelAddressProperty, value); }
        //}

        //public static readonly DependencyProperty ModelAddressProperty =
        //    DependencyProperty.Register("ModelAddress", typeof(string), typeof(IotDevice), new PropertyMetadata(null));

        public void SetModel()
        {
            foreach (RadioButton item in modlGroup.Items)
            {

            }
        }

        #endregion
    }
}
