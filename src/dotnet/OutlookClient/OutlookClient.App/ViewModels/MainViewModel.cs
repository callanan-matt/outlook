using Unity;

namespace OutlookClient.App.ViewModels
{
    public interface IMainViewModel
    {
        IEmailViewModel EmailViewModel { get; set; }
    }

    public class MainViewModel : IMainViewModel
    {
        [Dependency]
        public IEmailViewModel EmailViewModel { get; set; }
    }
}