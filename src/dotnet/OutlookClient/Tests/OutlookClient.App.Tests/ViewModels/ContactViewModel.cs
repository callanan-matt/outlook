using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OutlookClient.App.ViewModels
{
    public interface IContactViewModel
    {
        string Name { get; }
        string Location { get; }
        string Address { get; }
        string Phone { get; }
        string Company { get; }
        string Alias { get; }
        ICommand SaveCommand { get; }
    }

    public class ContactViewModel : BindableBase, IContactViewModel
    {
        private string _name;
        private string _location;
        private string _address;
        private string _phone;
        private string _company;
        private string _alias;

        public string Name { get => _name; set => SetProperty(ref _name, value, nameof(Name)); }

        public string Location { get => _location; set => SetProperty(ref _location, value, nameof(Location)); }

        public string Address { get => _address; set => SetProperty(ref _address, value, nameof(Address)); }

        public string Phone { get => _phone; set => SetProperty(ref _phone, value, nameof(Phone)); }

        public string Company { get => _company; set => SetProperty(ref _company, value, nameof(Company)); }

        public string Alias { get => _alias; set => SetProperty(ref _alias, value, nameof(Alias)); }
        public ICommand SaveCommand { get; }
    }
}
