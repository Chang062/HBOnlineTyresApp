using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace HBOnlineTyresApp.Data.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;

        public EmailSender(string smptServer, int smtpPort, string fromEmailAddress)
        {
            this.smtpPort = smtpPort;
            this.smtpServer = smptServer;
            this.fromEmailAddress  = fromEmailAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);            
            client.Send(message);

            return Task.CompletedTask;
            
        }
    }
}
