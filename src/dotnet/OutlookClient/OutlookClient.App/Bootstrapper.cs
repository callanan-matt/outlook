using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OutlookClient.App.ViewModels;
using OutlookClient.Services;
using OutlookClient.Services.Models;
using Prism.Logging;
using Unity;
using Unity.Lifetime;

namespace OutlookClient.App
{
    internal class Bootstrapper
    {
        internal static void Bootstrap(IUnityContainer container)
        {
            container
                .RegisterInstance<Func<FakeContact[]>>(
                    () => JsonConvert.DeserializeObject<FakeContact[]>("data.json"))
                .RegisterInstance<Func<FakeContact, IContact>>(
                    x => new Contact
                    {
                        EmailAddress = x.Email,
                        Address = x.Address,
                        Alias = x.Alias,
                        Company = x.Company,
                        Location = x.Location,
                        Name = x.Name,
                        Phone = x.Phone,
                        Thumbnail = x.Avatar
                    }, new SingletonLifetimeManager())
                .RegisterInstance<Action<IContact, ContactViewModel>>(
                    (m, vm) =>
                    {
                        vm.Name = m.Name;
                        vm.EmailAddress = m.EmailAddress;
                        vm.Company = m.Company;
                        vm.Address = m.Address;
                        vm.Alias = m.Alias;
                        vm.Location = m.Location;
                        vm.Phone = m.Phone;
                        vm.Thumbnail = m.Thumbnail;
                    }, new SingletonLifetimeManager())
                .RegisterInstance<Func<IContactViewModel, IContact>>(
                    x => new Contact
                    {
                        Name = x.Name, 
                        EmailAddress = x.EmailAddress, 
                        Company = x.Company,
                        Address = x.Address,
                        Alias = x.Alias,
                        Location = x.Location,
                        Phone = x.Phone,
                        Thumbnail = x.Thumbnail
                    }, new SingletonLifetimeManager())
                .RegisterInstance<Func<IContact, IContactViewModel>>(
                    x =>
                    {
                        var viewModel = container.Resolve<ContactViewModel>();
                        var mapper = container.Resolve<Action<IContact, ContactViewModel>>();
                        mapper(x, viewModel);
                        return viewModel;
                    }, new SingletonLifetimeManager())
                .RegisterInstance<Func<IContactListViewModel, IEnumerable<IContact>>>(
                    x =>
                    {
                        var modelFactory = container.Resolve<Func<IContactViewModel, IContact>>();
                        var contacts = new LinkedList<IContact>();
                        foreach(var item in x.Contacts)
                        {
                            var contact = modelFactory(item);
                            contacts.AddLast(contact);
                        }

                        return contacts;
                    }, new SingletonLifetimeManager())
                .RegisterType<ILoggerFacade, FakeLoggerFacade>(new SingletonLifetimeManager())
                .RegisterType<IEmailService, FakeEmailService>(new SingletonLifetimeManager())
                .RegisterType<IContactListViewModel, ContactListViewModel>(new TransientLifetimeManager())
                .RegisterType<IEmailViewModel, EmailViewModel>(new SingletonLifetimeManager())
                .RegisterType<IMainViewModel, MainViewModel>(new SingletonLifetimeManager());
        }
    }
}
