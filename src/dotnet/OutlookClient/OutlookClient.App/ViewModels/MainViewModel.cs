using Prism.Mvvm;
using Unity;

namespace OutlookClient.App.ViewModels
{
    public interface IMainViewModel
    {
        IEmailViewModel EmailViewModel { get; set; }
    }

    public class MainViewModel : BindableBase, IMainViewModel
    {
        private IEmailViewModel _emailViewModel;

        [Dependency]
        public IEmailViewModel EmailViewModel
        {
            get => _emailViewModel;
            set => SetProperty(ref _emailViewModel, value, nameof(EmailViewModel));
        }
    }
}