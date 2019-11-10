using Moq;
using NUnit.Framework;
using OutlookClient.Services;

namespace OutlookClient.App.ViewModels
{
    [TestFixture]
    public class EmailViewModelTests
    { 
        private EmailViewModel _target;
        private Mock<IEmailService> _emailService;

        [SetUp]
        public void Setup()
        {
            _emailService = new Mock<IEmailService>();
            _target = new EmailViewModel(_emailService.Object, x => Mappings.FromGroupViewModel(x, Mappings.FromViewModel));
        }
    }
}