using System;
using System.Collections.Generic;
using OutlookClient.App.ViewModels;
using OutlookClient.Services;
using OutlookClient.Services.Models;

namespace OutlookClient.App
{
    public static class Mappings
    {
        public static IContact FromFakeContact(FakeContact fakeContact)
        {
            return new Contact
            {
                EmailAddress = fakeContact.Email,
                Address = fakeContact.Address,
                Alias = fakeContact.Alias,
                Company = fakeContact.Company,
                Location = fakeContact.Location,
                Name = fakeContact.Name,
                Phone = fakeContact.Phone,
                Thumbnail = fakeContact.Avatar
            };
        }

        public static IContact FromViewModel(IContactViewModel viewModel)
        {
            return new Contact
            {
                Name = viewModel.Name,
                EmailAddress = viewModel.EmailAddress,
                Company = viewModel.Company,
                Address = viewModel.Address,
                Alias = viewModel.Alias,
                Location = viewModel.Location,
                Phone = viewModel.Phone,
                Thumbnail = viewModel.Thumbnail
            };
        }

        public static IContactViewModel ToViewModel(IContact contact, Func<IContactViewModel> viewModelFactory,
            Action<IContact, IContactViewModel> viewModelMapper)
        {
            var viewModel = viewModelFactory();
            viewModelMapper(contact, viewModel);
            return viewModel;
        }

        public static IEnumerable<IContact> FromGroupViewModel(IContactGroupViewModel viewModel,
            Func<IContactViewModel, IContact> itemFactory)
        {
            var contacts = new LinkedList<IContact>();
            foreach (var item in viewModel.Contacts)
            {
                var contact = itemFactory(item);
                contacts.AddLast(contact);
            }

            return contacts;
        }

        public static void MapToContact(IContact model, IContactViewModel viewModel)
        {
            viewModel.Name = model.Name;
            viewModel.EmailAddress = model.EmailAddress;
            viewModel.Company = model.Company;
            viewModel.Address = model.Address;
            viewModel.Alias = model.Alias;
            viewModel.Location = model.Location;
            viewModel.Phone = model.Phone;
            viewModel.Thumbnail = model.Thumbnail;
        }
    }
}