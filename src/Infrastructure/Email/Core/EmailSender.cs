using System;
using System.Threading.Tasks;
using CommandsRegistry.Infrastructure.Email.Core.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using static System.String;

namespace CommandsRegistry.Infrastructure.Email.Core
{
    internal sealed class EmailSender : IEmailSender
    {
        private readonly IMailConfiguration _mailConfiguration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IMailConfiguration mailConfiguration, ILogger<EmailSender> logger)
        {
            _mailConfiguration = mailConfiguration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string emailTo, string subject, string text)
        {
            if (!IsNullOrEmpty(_mailConfiguration.MailFrom))
            {
                var message = BuildMessage(emailTo, subject, text);
                await SendMessageAsync(message);
            }
        }

        private bool IsAuthenticationConfigured()
            => !IsNullOrEmpty(_mailConfiguration.ServerLogin) && !IsNullOrEmpty(_mailConfiguration.ServerPassword);

        private async Task SendMessageAsync(MimeMessage message)
        {
            using var client = new SmtpClient();

            if (_mailConfiguration.DisableSsl)
                DisableSsl(client);

            try
            {
                await client.ConnectAsync(_mailConfiguration.ServerAddress, _mailConfiguration.ServerPort);

                if (IsAuthenticationConfigured())
                    await client.AuthenticateAsync(_mailConfiguration.ServerLogin, _mailConfiguration.ServerPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        private MimeMessage BuildMessage(string emailTo, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            var mailFrom = IsAuthenticationConfigured()
                ? new MailboxAddress(_mailConfiguration.MailFrom, _mailConfiguration.ServerLogin)
                : MailboxAddress.Parse(_mailConfiguration.MailFrom);

            message.From.Add(mailFrom);
            message.To.Add(MailboxAddress.Parse(emailTo));

            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            };

            return message;
        }

        private static void DisableSsl(SmtpClient client)
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.CheckCertificateRevocation = false;
        }
    }
}