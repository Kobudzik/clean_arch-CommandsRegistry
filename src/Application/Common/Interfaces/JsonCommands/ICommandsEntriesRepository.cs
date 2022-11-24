using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.JsonCommands.Queries;
using CommandsRegistry.Application.JsonCommands.Queries.GetPaginatedJsonCommands;
using CommandsRegistry.Domain.Entities.Commands;

namespace CommandsRegistry.Application.Common.Interfaces.JsonCommands
{
    public interface ICommandsEntriesRepository
    {
        Task<CommandEntry> CreateAsync(string name, string jsonSchema);
        Task<CommandEntry> GetAsync(int id, bool expectToFind = false);
        Task<IEnumerable<CommandEntryDto>> GetListAsync();
        Task<PaginatedList<CommandEntryDto>> GetListAsync(Pager pager, GetPaginatedJsonCommandsFilterModel filter);
        Task Update(CommandEntryDto model);
        Task Delete(int id);
    }
}
