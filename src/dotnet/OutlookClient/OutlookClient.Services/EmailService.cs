using System;
using System.Collections.Generic;
using OutlookClient.Services.Models;

namespace OutlookClient.Services
{
    public interface IEmailService
    {
        void Populate(Action<IContact> onNext, Action<Exception> onError);
        void Find(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError);
        void FindByName(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError);
        void FindByAny(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError);
        void Send(string subject, string body, IEnumerable<IContact> toContacts, IEnumerable<IContact> ccContacts, IEnumerable<IContact> bccContacts, bool isBodyHtml);
    }

    public class EmailService : IEmailService
    {
        public void Populate(Action<IContact> onNext, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void Find(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void FindByName(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void FindByAny(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void Send(string subject, string body, IEnumerable<IContact> toContacts, IEnumerable<IContact> ccContacts, IEnumerable<IContact> bccContacts, bool isBodyHtml)
        {
            throw new NotImplementedException();
        }
    }
}
