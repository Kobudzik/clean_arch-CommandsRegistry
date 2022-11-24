using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Queries.GetJsonCommands
{
    public class GetJsonCommandsQuery: IRequest<IEnumerable<CommandEntryDto>>
    {
    }

    public class GetJsonCommandsQueryHandler : IRequestHandler<GetJsonCommandsQuery, IEnumerable<CommandEntryDto>>
    {
        private readonly ICommandsEntriesRepository _commandsEntriesRepository;

        public GetJsonCommandsQueryHandler(ICommandsEntriesRepository commandsEntriesRepository)
        {
            _commandsEntriesRepository = commandsEntriesRepository;
        }

        public async Task<IEnumerable<CommandEntryDto>> Handle(GetJsonCommandsQuery request, CancellationToken cancellationToken)
        {
            return await _commandsEntriesRepository.GetListAsync();
        }
    }
}
