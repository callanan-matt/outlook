﻿using Moq;
using NUnit.Framework;
using OutlookClient.Services;
using Prism.Logging;

namespace OutlookClient.App.ViewModels
{
    [TestFixture]
    public class ContactViewModelTests
    {
        private ContactViewModel _target;
        private Mock<IEmailService> _emailService;
        private Mock<ILoggerFacade> _logger;

        [SetUp]
        public void Setup()
        {
            _emailService = new Mock<IEmailService>();
            _logger = new Mock<ILoggerFacade>();
            _target = new ContactViewModel(_emailService.Object, _logger.Object, Mappings.MapToContact);
        }
    }
}