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
                .RegisterInstance<Func<FakeContact, IContact>>(Mappings.FromFakeContact, new SingletonLifetimeManager())
                .RegisterInstance<Action<IContact, IContactViewModel>>(Mappings.MapToContact, new SingletonLifetimeManager())
                .RegisterInstance<Func<IContactViewModel, IContact>>(Mappings.FromViewModel, new SingletonLifetimeManager())
                .RegisterInstance<Func<IContact, IContactViewModel>>(x => Mappings.ToViewModel(x, () => container.Resolve<IContactViewModel>(), Mappings.MapToContact), new SingletonLifetimeManager())
                .RegisterInstance<Func<IContactGroupViewModel, IEnumerable<IContact>>>(x => Mappings.FromGroupViewModel(x, Mappings.FromViewModel), new SingletonLifetimeManager())
                .RegisterType<ILoggerFacade, FakeLoggerFacade>(new SingletonLifetimeManager())
                .RegisterType<IEmailService, FakeEmailService>(new SingletonLifetimeManager())
                .RegisterType<IContactViewModel, ContactViewModel>(new TransientLifetimeManager())
                .RegisterType<IContactGroupViewModel, ContactGroupViewModel>(new TransientLifetimeManager())
                .RegisterType<IEmailViewModel, EmailViewModel>(new SingletonLifetimeManager())
                .RegisterType<IMainViewModel, MainViewModel>(new SingletonLifetimeManager());
        }
    }
}
