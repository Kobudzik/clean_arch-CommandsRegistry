using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Queries.GetJsonCommand
{
    public class GetJsonCommandQuery : IRequest<CommandEntryDto>
    {
        public GetJsonCommandQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetJsonCommandQueryHandler : IRequestHandler<GetJsonCommandQuery, CommandEntryDto>
    {
        private readonly ICommandsEntriesRepository _commandsEntriesRepository;
        private readonly IMapper _mapper;

        public GetJsonCommandQueryHandler(ICommandsEntriesRepository commandsEntriesRepository, IMapper mapper)
        {
            _commandsEntriesRepository = commandsEntriesRepository;
            _mapper = mapper;
        }

        public async Task<CommandEntryDto> Handle(GetJsonCommandQuery request, CancellationToken cancellationToken)
        {
            var entity = await _commandsEntriesRepository.GetAsync(request.Id);
            return _mapper.Map<CommandEntryDto>(entity);
        }
    }
}
