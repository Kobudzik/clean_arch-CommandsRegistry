using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Net.NetworkInformation;
using CommandsRegistry.Infrastructure.Persistence;
using CommandsRegistry.WebUI.Filters;
using CommandsRegistry.WebUI.Services;
using CommandsRegistry.Application;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Infrastructure;
using CommandsRegistry.Infrastructure.Email;

namespace CommandsRegistry.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEmailsModule();
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddCors();

            services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()
                )
            );

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommandsRegistry", Version = "v1" }));

            services.AddLogging(loggingBuilder =>
            {
                //var loggingSection = Configuration.GetSection("Logging");

                loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

                loggingBuilder.AddFile(
                    "logs/app_{0:yyyy}-{0:MM}-{0:dd}.log",
                    fileLoggerOpts => fileLoggerOpts.FormatLogFileName = fName => string.Format(fName, DateTime.Now)
                );
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherApp v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //app.UseHttpsRedirection();
            }

            // Re-write path only. / to /index.html
            //app.UseDefaultFiles();

            // Start serve files from wwwroot (Must be after UseDefaultFiles obviously.)
            //app.UseSpaStaticFiles();

            app.UseHealthChecks("/health");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Placed after normal routes, at end of pipeline, 
            // i.e. if request path did not match static content 
            // nor other routes, fallback to SPA
            //app.UseSpa(spa =>
            //{
            //    if (env.IsDevelopment())
            //        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
            //});
        }

        public static HealthCheckResult GetHealthByPinging(string address)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(address);

                    if (reply.Status != IPStatus.Success)
                        return HealthCheckResult.Unhealthy();

                    if (reply.RoundtripTime > 100)
                        return HealthCheckResult.Degraded();

                    return HealthCheckResult.Healthy();
                }
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}