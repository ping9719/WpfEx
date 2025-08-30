using Ping9719.WpfEx;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            base.ShutdownMode = ShutdownMode.OnMainWindowClose;
            base.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            var aaaa = MainLoadWindow.Show(new MainLoadInfo[]
             {
                 new MainLoadInfo (()=>{ System.Threading.Thread.Sleep(1000); },"11","11err"),
                 new MainLoadInfo (()=>{ System.Threading.Thread.Sleep(1000);int.Parse("aa"); },"22","122err",isErrIgnore:true),
                 new MainLoadInfo (()=>{ System.Threading.Thread.Sleep(1000); },"33","133err"),
                 new MainLoadInfo (()=>{ System.Threading.Thread.Sleep(1000); },"44","144err"),
             });

            mainWindow.Show();
        }
    }

}
