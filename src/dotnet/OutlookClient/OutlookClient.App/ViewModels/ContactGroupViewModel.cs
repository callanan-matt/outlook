using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OutlookClient.Services;
using OutlookClient.Services.Models;
using Prism.Commands;
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
        ICommand PopulateCommand { get; }
        ICommand SearchCommand { get; }
        ICommand AddSelectedSearchResultsCommand { get; }
        ICommand RemoveSelectedContactsCommand { get; }
    }

    public class ContactGroupViewModel : BindableBase, IContactGroupViewModel
    {
        private readonly IEmailService _emailService;
        private readonly Func<IContact, IContactViewModel> _contactViewModelFactory;
        private readonly ILoggerFacade _logger;
        private bool _isNameOnlySearch;
        private string _searchText;
        private ObservableCollection<IContactViewModel> _selectedSearchResults;
        private ObservableCollection<IContactViewModel> _selectedContacts;

        public ContactGroupViewModel(IEmailService emailService, ILoggerFacade logger, Func<IContact, IContactViewModel> contactViewModelFactory)
        {
            _emailService = emailService;
            _logger = logger;
            _contactViewModelFactory = contactViewModelFactory;
            Contacts = new ObservableCollection<IContactViewModel>();
            SearchResults = new ObservableCollection<IContactViewModel>();
            PopulateCommand = new DelegateCommand(OnPopulate);
            SearchCommand = new DelegateCommand(OnSearch, () => !string.IsNullOrWhiteSpace(SearchText));
            AddSelectedSearchResultsCommand = new DelegateCommand(OnAddContacts, () => SelectedSearchResults.Count > 0);
            RemoveSelectedContactsCommand = new DelegateCommand(OnRemoveContacts, () => SelectedContacts.Count > 0);
        }

        private void OnPopulate()
        {
            throw new NotImplementedException();
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

        public ICommand PopulateCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddSelectedSearchResultsCommand { get; }
        public ICommand RemoveSelectedContactsCommand { get; }
    }
}