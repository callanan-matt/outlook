using Moq;
using NUnit.Framework;
using OutlookClient.Services;
using Prism.Logging;

namespace OutlookClient.App.ViewModels
{
    [TestFixture]
    public class ContactGroupViewModelTests
    {
        private ContactGroupViewModel _target;
        private Mock<IEmailService> _emailService;
        private Mock<ILoggerFacade> _logger;

        [SetUp]
        public void Setup()
        {
            _emailService = new Mock<IEmailService>();
            _logger = new Mock<ILoggerFacade>();
            _target = new ContactGroupViewModel(_emailService.Object, _logger.Object,
                x => Mappings.ToViewModel(x, () => new Mock<IContactViewModel>().Object, Mappings.MapToContact));
        }
    }
}