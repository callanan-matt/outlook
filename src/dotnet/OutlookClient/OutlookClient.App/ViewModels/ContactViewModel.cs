using Prism.Mvvm;
using System;
using System.Windows.Input;
using OutlookClient.Services;
using OutlookClient.Services.Models;
using Prism.Commands;
using Prism.Logging;

namespace OutlookClient.App.ViewModels
{
    public interface IContactViewModel
    {
        string Name { get; }
        string EmailAddress { get; }
        string Location { get; }
        string Address { get; }
        string Phone { get; }
        string Company { get; }
        string Alias { get; }
        string Thumbnail { get; }
        string SearchText { get; set; }
        ICommand SaveCommand { get; }
        ICommand SearchCommand { get; }
    }

    public class ContactViewModel : BindableBase, IContactViewModel
    {
        private readonly IEmailService _emailService;
        private readonly ILoggerFacade _logger;
        private readonly Action<IContact, ContactViewModel> _mapper;
        private string _name;
        private string _location;
        private string _address;
        private string _phone;
        private string _company;
        private string _alias;
        private string _thumbnail;
        private string _emailAddress;
        private string _searchText;

        public ContactViewModel(IEmailService emailService, ILoggerFacade logger, Action<IContact, ContactViewModel> mapper)
        {
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
            SaveCommand = new DelegateCommand(OnSave, () => false);
            SearchCommand = new DelegateCommand(OnSearch, () => !string.IsNullOrWhiteSpace(SearchText));
        }

        private void OnError(Exception e)
        {
            _logger.Log(e.ToString(), Category.Exception, Priority.High);
        }

        private void OnSearch()
        {
            var hasSingleResult = false;
            _emailService.Find(SearchText, x => hasSingleResult = x == 1, x =>
            {
                if (!hasSingleResult) return;
                _mapper(x, this);
            }, OnError);
        }

        private void OnSave()
        {
            throw new NotSupportedException();
        }

        public string Name
        {
            get => _name; 
            set => SetProperty(ref _name, value, nameof(Name));
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value, nameof(EmailAddress));
        }

        public string Location
        {
            get => _location; 
            set => SetProperty(ref _location, value, nameof(Location));
        }

        public string Address
        {
            get => _address; 
            set => SetProperty(ref _address, value, nameof(Address));
        }

        public string Phone
        {
            get => _phone; 
            set => SetProperty(ref _phone, value, nameof(Phone));
        }

        public string Company
        {
            get => _company; 
            set => SetProperty(ref _company, value, nameof(Company));
        }

        public string Alias
        {
            get => _alias; 
            set => SetProperty(ref _alias, value, nameof(Alias));
        }

        public string Thumbnail
        {
            get => _thumbnail; 
            set => SetProperty(ref _thumbnail, value, nameof(Thumbnail));
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }
    }
}