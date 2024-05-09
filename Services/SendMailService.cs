using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

public class MailSettings
{
    public string Mail { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }

}

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendSmsAsync(string number, string message);
}

public class SendMailService : IEmailSender
{


    private readonly MailSettings mailSettings;

    private readonly ILogger<SendMailService> logger;


    // mailSetting được Inject qua dịch vụ hệ thống
    // Có inject Logger để xuất log
    public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
    {
        mailSettings = _mailSettings.Value;
        logger = _logger;
        logger.LogInformation("Create SendMailService");
    }


    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrWhiteSpace(mailSettings.Mail))
        {
            logger.LogError("Mail setting is invalid or empty.");
            return;
        }

        var message = new MimeMessage();
        message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
        message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = htmlMessage;
        message.Body = builder.ToMessageBody();

        // Lưu nội dung email vào thư mục mailssave trước
        System.IO.Directory.CreateDirectory("mailssave");
        var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
        await message.WriteToAsync(emailsavefile);
        logger.LogInformation("Đã lưu nội dung email vào - " + emailsavefile);

        // Cố gắng gửi email
        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(message);
            logger.LogInformation("Đã gửi email tới " + email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
        finally
        {
            smtp.Disconnect(true);
        }
    }
    public Task SendSmsAsync(string number, string message)
    {
        // Cài đặt dịch vụ gửi SMS tại đây
        System.IO.Directory.CreateDirectory("smssave");
        var emailsavefile = string.Format(@"smssave/{0}-{1}.txt", number, Guid.NewGuid());
        System.IO.File.WriteAllTextAsync(emailsavefile, message);
        return Task.FromResult(0);
    }
}