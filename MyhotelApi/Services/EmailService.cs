using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MyhotelApi.Objects.Options;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Services.IServices;
using System.Net;
using MyhotelApi.Helpers.AddServiceFromAttribute;

namespace MyhotelApi.Services;

[Scoped]
public class EmailService : IEmailService
{
    private readonly EmailConfiguration emailConfig;
    private readonly EmailBody emailBody;

    public EmailService(IOptions<EmailConfiguration> emailConfig, IOptions<EmailBody> emailBody)
    {
        this.emailConfig = emailConfig.Value;
        this.emailBody = emailBody.Value;
    }

    public void SendEmail(string[] receiverEmail, string contentMessage)
    {
        var email = CreateEmailMessage(receiverEmail, contentMessage);
        SendEmailMessage(email);
    }

    private MimeMessage CreateEmailMessage(string[]? receiverEmail, string contentMessage)
    {
        var mimeMassage = new MimeMessage();

        mimeMassage.To.AddRange(receiverEmail!.Select(e => new MailboxAddress("", e)));
        mimeMassage.From.Add(new MailboxAddress("email", emailConfig.From));
        mimeMassage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = contentMessage };
        mimeMassage.Subject = emailBody.Subject;

        return mimeMassage;
    }

    private void SendEmailMessage(MimeMessage mimeMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                var credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
                client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(credentials);
                var response = client.Send(mimeMessage);
            }
            catch
            {
                throw new NotFoundException("Email related problem");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}