using CommandsRegistry.Application.Authentication;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Infrastructure.Identity.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsRegistry.Infrastructure.Identity.Users
{
    internal static class UserManagementModule
    {
        internal static void AddUserManagementModule(this IServiceCollection services)
        {
            //services.AddSingleton<IUsersConfiguration, UsersConfiguration>();
            //services.AddSingleton<IAdminAccountConfiguration, AdminAccountConfiguration>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ISignInManagementService, SignInManagementService>();
            services.AddScoped<IRoleManagementService, RoleManagementService>();
            //services.AddScoped<IPasswordComparer, PasswordComparer>();
            services.AddScoped<IPasswordsManagementService, PasswordsManagementService>();
            //services.AddScoped<IUsersSeeder, UsersSeeder>();
        }
    }
}