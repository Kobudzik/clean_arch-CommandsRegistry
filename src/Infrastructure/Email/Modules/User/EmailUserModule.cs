using CommandsRegistry.Application.Common.Interfaces.Email.User.ConfirmEmail;
using CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail;
using CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsRegistry.Infrastructure.Email.Modules.User
{
    internal static class EmailUserModule
    {
        internal static IServiceCollection AddUserEmails(this IServiceCollection services)
        {
            services.AddSingleton<IConfirmEmailConfiguration, ConfirmEmailConfiguration>();
            services.AddScoped<IConfirmEmailSender, ConfirmEmailSender>();

            return services;
        }
    }
}