using Microsoft.Extensions.Options;
using System.Net.Mail;
using Data.Entities;
using Data.Repositories;

public interface IEmailService
{
    void SendEmail(string toEmail, string subject, string body);
    void SendWelcomeEmail(User user, string password);
}

public class EmailService : IEmailService
{
    private readonly Repository _repository;
    private readonly SMTPSetting _smtpSettings;

    public EmailService(Repository repository, IOptions<SMTPSetting> smtpSettings)
    {
        _repository = repository;
        _smtpSettings = smtpSettings.Value;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(_smtpSettings.Server)
        {
            Port = _smtpSettings.Port,
            Credentials = new System.Net.NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.FromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(toEmail);

        smtpClient.Send(mailMessage);
    }

    public void SendWelcomeEmail(User user, string password)
    {
        var template = _repository.EmailTemplates
            .FirstOrDefault(t => t.Name == "RegistrationSuccess");

        if (template == null) throw new Exception("Email template not found");

        var body = template.Body
            .Replace("{Name}", user.Name)
            .Replace("{Email}", user.Email)
            .Replace("{Password}", password);

        SendEmail(user.Email, template.Subject, body);
    }
}