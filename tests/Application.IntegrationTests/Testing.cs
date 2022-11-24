using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Domain.Entities.Core;
using CommandsRegistry.Infrastructure.Identity;
using CommandsRegistry.Infrastructure.Persistence;
using CommandsRegistry.WebUI;

namespace CommandsRegistry.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static string _currentUserId;

        [OneTimeSetUp]
        public void RunBeforeTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "CommandsRegistry.WebUI"));

            services.AddLogging();

            startup.ConfigureServices(services);

            services.AddSingleton<IConfiguration>(_configuration);

            #region current user service
            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(_ =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));
            #endregion current user service

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase().GetAwaiter().GetResult();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory", "Roles" }
            };
        }

        /// <summary>
        /// Creates ApplicationDbContext service and migrates DB
        /// </summary>
        private static async Task EnsureDatabase()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserAccount>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.Migrate();
                await ApplicationDbContextSeed.SeedDefaultUserAndRolesAsync(userManager, roleManager);
            }
        }

        /// <summary>
        /// Resets DB state to initial
        /// </summary>
        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

            _currentUserId = null;
        }

        /// <summary>
        /// Sends mediator request
        /// </summary>
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        #region RUN AS
        /// <summary>
        /// Creates userManagerService, creates new user with roles, sets _currentUserId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static async Task<string> RunAsUserAsync(string username, string password, string[] roles)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<UserAccount>>();

            var user = UserAccount.Create(username, username, username, username, true);

            var result = await userManager.CreateAsync(user, password);

            if (roles.Length > 0)
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRolesAsync(user, roles);
            }

            if (result.Succeeded)
            {
                _currentUserId = user.Id;

                return _currentUserId;
            }

            var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

            throw new Exception($"Unable to create {username}.{Environment.NewLine}{errors}");
        }

        public static async Task<string> RunAsRegularUserAsync()
        {
            return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
        }

        public static async Task<string> RunAsAdministratorAsync()
        {
            return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
        }
        #endregion RUN AS

        #region CRUD
        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<int> CountAsync<TEntity>()
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }
        #endregion CRUD

        //[OneTimeTearDown]
        //public void RunAfterAnyTests()
        //{
        //}
    }
}