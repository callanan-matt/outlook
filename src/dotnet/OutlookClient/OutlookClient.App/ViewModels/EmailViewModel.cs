using System;
using System.Collections.Generic;
using System.Windows.Input;
using OutlookClient.Services;
using OutlookClient.Services.Models;
using Prism.Commands;
using Prism.Mvvm;
using Unity;

namespace OutlookClient.App.ViewModels
{
    public interface IEmailViewModel
    {
        IContactListViewModel ToContactList { get; set; }
        IContactListViewModel CcContactList { get; set; }
        IContactListViewModel BccContactList { get; set; }
        IContactListViewModel SelectedContactList { get; }
        string Subject { get; set; }
        string Body { get; set; }
        ICommand SendMailCommand { get; }
    }

    public class EmailViewModel : BindableBase, IEmailViewModel
    {
        private readonly IEmailService _service;
        private readonly Func<IContactListViewModel, IEnumerable<IContact>> _contactFactory;
        private IContactListViewModel _toContactList;
        private IContactListViewModel _ccContactList;
        private IContactListViewModel _bccContactList;
        private IContactListViewModel _selectedContactList;
        private string _subject;
        private string _body;

        public EmailViewModel(IEmailService service, Func<IContactListViewModel, IEnumerable<IContact>> contactFactory)
        {
            _service = service;
            _contactFactory = contactFactory;
            SendMailCommand = new DelegateCommand(OnSendMail, CanSendMail);
        }

        private void OnSendMail()
        {
            _service.Send(Subject, Body, _contactFactory(ToContactList), _contactFactory(CcContactList), _contactFactory(BccContactList), false);
        }

        private bool CanSendMail()
        {
            return !string.IsNullOrWhiteSpace(Subject) && !string.IsNullOrWhiteSpace(Body) && ToContactList.Contacts.Count > 0;
        }

        [Dependency]
        public IContactListViewModel ToContactList
        {
            get => _toContactList; 
            set => SetProperty(ref _toContactList, value, nameof(ToContactList));
        }

        [Dependency]
        public IContactListViewModel CcContactList
        {
            get => _ccContactList;
            set => SetProperty(ref _ccContactList, value, nameof(CcContactList));
        }

        [Dependency]
        public IContactListViewModel BccContactList
        {
            get => _bccContactList; 
            set => SetProperty(ref _bccContactList, value, nameof(BccContactList));
        }

        public IContactListViewModel SelectedContactList
        {
            get => _selectedContactList; 
            private set => SetProperty(ref _selectedContactList, value, nameof(SelectedContactList));
        }

        public string Subject
        {
            get => _subject;
            set => SetProperty(ref _subject, value, nameof(Subject));
        }

        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value, nameof(Body));
        }

        public ICommand SendMailCommand { get; }
    }
}