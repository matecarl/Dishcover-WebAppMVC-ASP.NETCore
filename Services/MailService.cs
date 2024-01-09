using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NETCore.MailKit.Infrastructure.Internal;

namespace Dishcover.Services
{
    public class MailService : IEmailSender
    {
        public MailKitOptions Options { get; set; }

        public MailService(IOptions<MailKitOptions> options)
        {
            this.Options = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public Task Execute(string recipient, string subject, string htmlMessage)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(Options.SenderEmail);
            email.Sender.Name = Options.SenderName;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };
        
            using (var smtp = new SmtpClient())
            {
                SecureSocketOptions Security = SecureSocketOptions.None;
                
                if (Options.Security)
                {
                    if (Options.Port == 587)
                        Security = SecureSocketOptions.StartTls;
                    else
                        Security = SecureSocketOptions.SslOnConnect;
                }

                smtp.Connect(Options.Server, Options.Port, Security);
                smtp.Authenticate(Options.Account, Options.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return Task.FromResult(true);
        }
    }
}
