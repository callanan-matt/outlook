using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using OutlookClient.Services.Models;

namespace OutlookClient.Services
{
    [DataContract]
    public class FakeContact
    {
        /* 
    "id": "46",
    "createdAt": "2019-11-09T15:42:31.943Z",
    "name": "Clyde Doyle",
    "avatar": "https://s3.amazonaws.com/uifaces/faces/twitter/joshmedeski/128.jpg"
	"location": "Hong Kong", 
	"company": "Legal", 
	"address": "Shanghai Ave.", 
	"phone": "*02-21-914-2990", 
	"alias": "cdoyle", 
    "email": "cdoyle@company.com"
         */
        [DataMember(Name = "id")]
        public string Identifier { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "location")]
        public string Location { get; set; }
        [DataMember(Name = "company")]
        public string Company { get; set; }
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }
        [DataMember(Name = "alias")]
        public string Alias { get; set; }
    }

    public class FakeEmailService : IEmailService
    {
        private readonly Func<FakeContact[]> _dataGetter;
        private readonly Func<FakeContact, IContact> _contactFactory;
        private bool _isDataLoaded;
        private FakeContact[] _data;

        public FakeEmailService(Func<FakeContact[]> dataGetter, Func<FakeContact, IContact> contactFactory)
        {
            _dataGetter = dataGetter;
            _contactFactory = contactFactory;
        }

        private void LoadData()
        {
            _data = _dataGetter();
            _isDataLoaded = true;
        }

        private void TryLoadData(Action<Exception> onError)
        {
            try
            {
                LoadData();
            }
            catch (Exception e)
            {
                onError(e);
            }
        }

        private void TryFind(Action<int> onCount, Action<IContact> onNext, Action<Exception> onError, Func<FakeContact, bool> findCriteria)
        {
            if (!_isDataLoaded) TryLoadData(onError);
            var results = _data.Where(findCriteria).ToArray();
            onCount(results.Length);
            foreach (var item in results)
            {
                var contact = _contactFactory(item);
                onNext(contact);
            }
        }

        public void Populate(Action<IContact> onNext, Action<Exception> onError)
        {
            if (!_isDataLoaded) TryLoadData(onError);
            foreach (var item in _data)
            {
                var contact = _contactFactory(item);
                onNext(contact);
            }
        }

        public void Find(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            if (string.IsNullOrWhiteSpace(search))
                throw new ArgumentException("No search criteria provided.", nameof(search));
            TryFind(onCount, onNext, onError, x => x.Alias.StartsWith(search) || 
                                                   x.Name.StartsWith(search) ||
                                                   x.Email.StartsWith(search));
        }

        public void FindByName(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            if (string.IsNullOrWhiteSpace(search))
                throw new ArgumentException("No search criteria provided.", nameof(search));
            TryFind(onCount, onNext, onError, x => x.Name.StartsWith(search));
        }

        public void FindByAny(string search, Action<int> onCount, Action<IContact> onNext, Action<Exception> onError)
        {
            if (string.IsNullOrWhiteSpace(search))
                throw new ArgumentException("No search criteria provided.", nameof(search));
            TryFind(onCount, onNext, onError, x => x.Alias.StartsWith(search) ||
                                                   x.Name.StartsWith(search) ||
                                                   x.Email.StartsWith(search) || 
                                                   x.Address.StartsWith(search) || 
                                                   x.Company.StartsWith(search) || 
                                                   x.Location.StartsWith(search) || 
                                                   x.Phone.StartsWith(search));
        }

        public void Send(string subject, string body, IEnumerable<IContact> toContacts, IEnumerable<IContact> ccContacts, IEnumerable<IContact> bccContacts, bool isBodyHtml)
        {
            Console.WriteLine(">>> SENDING EMAIL <<<");
            Console.WriteLine("SUBJECT: {0}", subject);
            Console.WriteLine("TO: {0}", string.Join(",", toContacts));
            Console.WriteLine("CC: {0}", string.Join(",", ccContacts));
            Console.WriteLine("BCC: {0}", string.Join(",", bccContacts));
            Console.WriteLine("BODY: {0}", body);
        }
    }
}
