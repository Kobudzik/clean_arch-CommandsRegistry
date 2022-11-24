using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using CommandsRegistry.Application.Common.Models;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Queries.GetPaginatedJsonCommands
{
    public class GetPaginatedJsonCommandsQuery: IRequest<PaginatedList<CommandEntryDto>>
    {
        public GetPaginatedJsonCommandsQuery(Pager pager, GetPaginatedJsonCommandsFilterModel filter)
        {
            Pager = pager;
            Filter = filter;
        }

        public Pager Pager { get; }
        public GetPaginatedJsonCommandsFilterModel Filter { get; }
    }

    public class GetPlacesWithPaginationQueryHandler : IRequestHandler<GetPaginatedJsonCommandsQuery, PaginatedList<CommandEntryDto>>
    {
        private readonly ICommandsEntriesRepository _commandsEntriesRepository;

        public GetPlacesWithPaginationQueryHandler(ICommandsEntriesRepository commandsEntriesRepository)
        {
            _commandsEntriesRepository = commandsEntriesRepository;
        }

        public async Task<PaginatedList<CommandEntryDto>> Handle(GetPaginatedJsonCommandsQuery request, CancellationToken cancellationToken)
        {
            return await _commandsEntriesRepository.GetListAsync(request.Pager, request.Filter);
        }
    }
}
