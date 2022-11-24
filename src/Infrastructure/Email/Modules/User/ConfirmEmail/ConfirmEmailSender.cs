using System;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.Email.User.ConfirmEmail;
using CommandsRegistry.Infrastructure.Configuration;
using CommandsRegistry.Infrastructure.Configuration.Application;
using CommandsRegistry.Infrastructure.Email.Core;
using CommandsRegistry.Infrastructure.Email.Core.TemplateReader;
using CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail.Configuration;

namespace CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail
{
    internal class ConfirmEmailSender : IConfirmEmailSender
    {
        private readonly IConfirmEmailConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IMailTemplateReader _mailTemplateReader;
        private readonly IAplicationConfiguration aplicationConfiguration;

        public ConfirmEmailSender(
            IConfirmEmailConfiguration configuration,
            IEmailSender emailSender,
            IMailTemplateReader mailTemplateReader, IAplicationConfiguration aplicationConfiguration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
            _mailTemplateReader = mailTemplateReader;
            this.aplicationConfiguration = aplicationConfiguration;
        }

        public async Task SendConfirmationEmail(string email, string username, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(_configuration.PathToTemplate))
                throw new InvalidOperationException("ConfirmationEmail PathToTemplate is not set");

            if (string.IsNullOrEmpty(aplicationConfiguration.FrontendUrl))
                throw new InvalidOperationException("AplicationConfiguration FrontendUrl is not set");

            const string usernamePlaceheholder = "{Username}";
            const string tokenPlaceholder = "{TokenPlaceholder}";
            const string userIdPlaceholder = "{UserIdPlaceholder}";
            const string frontendPlaceholder = "{FrontendUrl}";

            var formattedToken = token.Replace("+", "%2B");

            var htmlTemplate = _mailTemplateReader.Read(_configuration.PathToTemplate);

            var emailAsHtml = htmlTemplate
                .Replace(usernamePlaceheholder, username)
                .Replace(tokenPlaceholder, formattedToken)
                .Replace(userIdPlaceholder, userId)
                .Replace(frontendPlaceholder, aplicationConfiguration.FrontendUrl);

            await _emailSender.SendEmailAsync(email, _configuration.Subject, emailAsHtml);
        }
    }
}