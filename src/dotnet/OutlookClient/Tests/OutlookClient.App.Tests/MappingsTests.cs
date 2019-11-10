using NUnit.Framework;
using OutlookClient.Services;

namespace OutlookClient.App
{
    [TestFixture]
    public class MappingsTests
    {
        [Test(Description = "Test that when FromFakeContact is called a contact is returned with data populated.")]
        public void TestFromFakeContactReturnsNewContactWithPopulatedValues()
        {
            var fakeContact = new FakeContact
            {
                Address = "123 main st.",
                Email = "hello@test.org",
                Alias = "some_login",
                Company = "Legal",
                Location = "New York",
                Name = "Melissa Doe",
                Avatar = "https://thisiswheremyavataris.com/myavatar.jpg",
                Phone = "123-4567"
            };
            var contact = Mappings.FromFakeContact(fakeContact);
            Assert.IsNotNull(contact);
            Assert.AreEqual(fakeContact.Email, contact.EmailAddress);
            Assert.AreEqual(fakeContact.Address, contact.Address);
            Assert.AreEqual(fakeContact.Alias, contact.Alias);
            Assert.AreEqual(fakeContact.Company, contact.Company);
            Assert.AreEqual(fakeContact.Location, contact.Location);
            Assert.AreEqual(fakeContact.Name, contact.Name);
            Assert.AreEqual(fakeContact.Phone, contact.Phone);
            Assert.AreEqual(fakeContact.Avatar, contact.Thumbnail);
        }
    }
}