using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Commands.CreateCommandEntry
{
    public class CreateCommandEntryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string JsonSchema { get; set; }

        internal sealed class Handler : IRequestHandler<CreateCommandEntryCommand, int>
        {
            private readonly ICommandsEntriesRepository _commandsEntriesRepository;

            public Handler(ICommandsEntriesRepository commandsEntriesRepository)
            {
                _commandsEntriesRepository = commandsEntriesRepository;
            }

            public async Task<int> Handle(CreateCommandEntryCommand request, CancellationToken cancellationToken)
            {
                var commandEntry = await _commandsEntriesRepository.CreateAsync(request.Name, request.JsonSchema);
                return commandEntry.Id;
            }
        }
    }
}
