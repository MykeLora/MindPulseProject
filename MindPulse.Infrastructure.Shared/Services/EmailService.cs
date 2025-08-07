using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MindPulse.Core.Application.DTOs.Email;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Domain.Settings;
using System;
using System.Threading.Tasks;


namespace MindPulse.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailRequest.Body))
                    throw new ArgumentException("El cuerpo del correo (Body) no puede ser nulo o vacío.");

                var message = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(emailRequest.From ?? EmailSettings.EmailFrom)
                };

                message.To.Add(MailboxAddress.Parse(emailRequest.To));
                message.Subject = emailRequest.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = emailRequest.Body
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(EmailSettings.SmtpHost, EmailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(EmailSettings.SmtpUser, EmailSettings.SmtpPass);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar el correo: {ex.Message}", ex);
            }
        }

    }

}
