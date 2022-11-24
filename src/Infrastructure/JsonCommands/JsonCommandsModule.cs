using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using CommandsRegistry.Infrastructure.JsonCommands.CommandEntries;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsRegistry.Infrastructure.JsonCommands
{
    internal static class JsonCommandsModule
    {
        /// <summary>
        /// Registers JsonCommands services
        /// </summary>
        internal static void AddJsonCommandsModule(this IServiceCollection services)
        {
            services.AddScoped<ICommandsEntriesRepository, CommandsEntriesRepository>();
        }
    }
}