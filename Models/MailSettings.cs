using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WebAnToanVeSinhThucPhamDemo.Models
{
    public class MailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SendMailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public SendMailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.Sender));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_mailSettings.MailServer, _mailSettings.MailPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
                await client.SendAsync(mimeMessage);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new InvalidOperationException(ex.Message);
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Implement or leave it as not implemented if SMS service is not needed.
            throw new NotImplementedException();
        }
    }
}
