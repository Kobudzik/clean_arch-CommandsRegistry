using Microsoft.Extensions.DependencyInjection;
using CommandsRegistry.Infrastructure.Email.Core;
using CommandsRegistry.Infrastructure.Email.Modules.User;

namespace CommandsRegistry.Infrastructure.Email
{
    public static class EmailsModule
    {
        public static IServiceCollection AddEmailsModule(this IServiceCollection services)
        {
            services.AddEmailCore();
            services.AddUserEmails();

            return services;
        }
    }
}