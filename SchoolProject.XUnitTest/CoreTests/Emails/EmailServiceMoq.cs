using FluentAssertions;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Moq;
using Moq.Protected;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.CoreTests.Emails
{
    public class EmailServiceMoq 
    {
        private readonly Mock<SmtpClient> _smtpClientMock;
        private readonly EmailSettings _settings;
        private readonly EmailService _emailService;
        public EmailServiceMoq()
        {
            _settings = new EmailSettings()
            {
             
                Port = 587,
                Host = "smtp.gmail.com",
                FromEmail = "test@example.com",
                Password = "TestPassword"
            };
            _smtpClientMock = new Mock<SmtpClient>();
            _emailService = new EmailService(_settings);
        }
    //    [Fact]
    //    public async Task SendEmail_Should_Return_Success_When_Email_Is_Sent()
    //    {
    //        //Arrange
    //        var email = "testRahma@gmail.com";
    //        var Message = "Helllo";
    //        var reason = "Testtt";
    //        _smtpClientMock.Setup(x => x.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.Auto, It.IsAny<CancellationToken>()))
    //                  .Returns(Task.CompletedTask);

         
    //        _smtpClientMock.Setup(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
    //                   .Returns(Task.FromResult("Success"));
    //        _smtpClientMock.Setup(x=>x.DisconnectAsync(true, It.IsAny<CancellationToken>()))
    //            .Returns(Task.CompletedTask);
    //        //Assert
    //        var result= await _emailService.SendEmail(email,Message,reason);
    //        //Act
    //        result.Should().Be("Success");
    //      //  _smtpClientMock.Verify(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>(), Times.Once);
    //    }
    }
}
