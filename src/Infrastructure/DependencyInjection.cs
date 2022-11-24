using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Infrastructure.Configuration;
using CommandsRegistry.Infrastructure.Configuration.Application;
using CommandsRegistry.Infrastructure.Persistence;
using CommandsRegistry.Infrastructure.Services;
using CommandsRegistry.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommandsRegistry.Infrastructure.Identity.Users;
using CommandsRegistry.Infrastructure.Identity.Jwt;

namespace CommandsRegistry.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Infrastructure's DI, Databases
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CommandsRegistry"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            //sqlOptions.MigrationsAssembly("Infrastructure.Persistence.CoreMigrations");
                        }
                    ),
                    ServiceLifetime.Transient
              );
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddIdentity();

            services.AddJwtTokenAuthorizationModule(configuration);

            services.AddUserManagementModule();

            services.AddSingleton<IAplicationConfiguration, AplicationConfiguration>();

            return services;
        }
    }
}