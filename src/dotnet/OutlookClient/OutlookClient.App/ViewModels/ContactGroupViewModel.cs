using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OutlookClient.Services;
using OutlookClient.Services.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;

namespace OutlookClient.App.ViewModels
{
    public interface IContactGroupViewModel
    {
        bool IsNameOnlySearch { get; set; }
        string SearchText { get; set; }
        ObservableCollection<IContactViewModel> Contacts { get; }
        ObservableCollection<IContactViewModel> SelectedContacts { get; }
        ObservableCollection<IContactViewModel> SearchResults { get; }
        ObservableCollection<IContactViewModel> SelectedSearchResults { get; }
        ICommand SearchCommand { get; }
        ICommand AddSelectedSearchResults { get; }
        ICommand RemoveSelectedContacts { get; }
    }

    public class ContactGroupViewModel : BindableBase, IContactGroupViewModel
    {
        private readonly IEmailService _emailService;
        private readonly Func<IContact, IContactViewModel> _contactViewModelFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly Action<Exception> _onError;
        private bool _isNameOnlySearch;
        private string _searchText;
        private ObservableCollection<IContactViewModel> _selectedSearchResults;
        private ObservableCollection<IContactViewModel> _selectedContacts;

        public ContactGroupViewModel(IEmailService emailService, Func<IContact, IContactViewModel> contactViewModelFactory, ILoggerFacade logger)
        {
            _emailService = emailService;
            _contactViewModelFactory = contactViewModelFactory;
            _logger = logger;
            Contacts = new ObservableCollection<IContactViewModel>();
            SearchResults = new ObservableCollection<IContactViewModel>();
            SearchCommand = new DelegateCommand(OnSearch, () => !string.IsNullOrWhiteSpace(SearchText));
            AddSelectedSearchResults = new DelegateCommand(OnAddContacts, () => SelectedSearchResults.Count > 0);
            RemoveSelectedContacts = new DelegateCommand(OnRemoveContacts, () => SelectedContacts.Count > 0);
        }

        private void OnRemoveContacts()
        {
            throw new NotImplementedException();
        }

        private void OnAddContacts()
        {
            throw new NotImplementedException();
        }

        private void OnCount(int count)
        {
            _logger.Log($"Find returned {count} results.", Category.Info, Priority.Medium);
        }

        private void OnError(Exception e)
        {
            _logger.Log(e.ToString(), Category.Exception, Priority.High);
        }

        private void OnSearch()
        {
            if(IsNameOnlySearch)
                _emailService.FindByName(SearchText, OnCount, x => _contactViewModelFactory(x), OnError);
            else
                _emailService.FindByAny(SearchText, OnCount, x => _contactViewModelFactory(x), OnError);
        }

        public bool IsNameOnlySearch
        {
            get => _isNameOnlySearch;
            set => SetProperty(ref _isNameOnlySearch, value, nameof(IsNameOnlySearch));
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        public ObservableCollection<IContactViewModel> Contacts { get; }

        public ObservableCollection<IContactViewModel> SelectedContacts
        {
            get => _selectedContacts;
            set => SetProperty(ref _selectedContacts, value, nameof(SelectedContacts));
        }

        public ObservableCollection<IContactViewModel> SearchResults { get; }

        public ObservableCollection<IContactViewModel> SelectedSearchResults
        {
            get => _selectedSearchResults;
            set => SetProperty(ref _selectedSearchResults, value, nameof(SelectedSearchResults));
        }

        public ICommand SearchCommand { get; }
        public ICommand AddSelectedSearchResults { get; }
        public ICommand RemoveSelectedContacts { get; }
    }
}