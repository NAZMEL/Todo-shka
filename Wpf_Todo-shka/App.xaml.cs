using System.Windows;
using static Wpf_Todo_shka.MainWindow;

namespace Wpf_Todo_shka
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {        
            this.Exit += new ExitEventHandler(App_Exit); 
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            excer.DBClose();
        }
    }
}
