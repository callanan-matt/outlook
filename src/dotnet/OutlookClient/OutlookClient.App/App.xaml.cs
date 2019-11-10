using System.Windows;
using OutlookClient.App.ViewModels;
using Unity;

namespace OutlookClient.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();
            Bootstrapper.Bootstrap(container);

            var viewModel = container.Resolve<IMainViewModel>();
            MainWindow = new MainWindow {DataContext = viewModel};
            MainWindow.Show();
        }
    }
}
