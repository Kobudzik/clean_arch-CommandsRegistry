using CommandsRegistry.Infrastructure.Email.Core.Configuration;
using CommandsRegistry.Infrastructure.Email.Core.TemplateReader;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsRegistry.Infrastructure.Email.Core
{
    internal static class EmailCoreModule
    {
        internal static IServiceCollection AddEmailCore(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IMailConfiguration, MailConfiguration>();
            services.AddScoped<IMailTemplateReader, MailTemplateReader>();

            return services;
        }
    }
}