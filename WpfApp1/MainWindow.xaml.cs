using Ping9719.WpfEx;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var aaa = MessageBoxTipInput.Show(type: "datatime", "asadasd", (a) => 
            {
                if (a is Int16)
                {
                    return "sad";
                }
                return "";
            });
            //MessageBoxTip.Show("12312");

        }
    }
}