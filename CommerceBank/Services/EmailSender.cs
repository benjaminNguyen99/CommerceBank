using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System;

namespace CommerceBank.Services
{
    public class EmailSender :IEmailSender
    {
        public EmailSender (IOptions<EmailConfirmCode> optionAccessor)
        {
            Options = optionAccessor.Value;
        }
        public EmailConfirmCode Options { get; set; }

        public Task SendEmailAsync (string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
         }

        private Task Execute(string APIKey, string subject, string message, string email)
        {
            var client = new SendGridClient(APIKey);
            var mess = new SendGridMessage()
            {
                From = new EmailAddress("pdnrtv@umsystem.edu", "Team Project is Loading"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,

            };
            mess.AddTo(new EmailAddress(email));
            mess.SetClickTracking(false, false);
            return client.SendEmailAsync(mess);
        }
    }
}
