using System.Configuration;
using System.Data;
using System.Reflection.Metadata;
using System.Windows;
using TrueDiplom.Views;

namespace TrueDiplom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var win = new MainWindow();
            win.Show();
        }
        void OnMainWindowClosed()
        {
            Shutdown();
        }
    }

}
