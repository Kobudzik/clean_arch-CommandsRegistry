using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using CommandsRegistry.Application.JsonCommands.Queries;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Commands.UpdateCommandEntry
{
    public class UpdateCommandEntryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JsonSchema { get; set; }

        internal sealed class Handler : IRequestHandler<UpdateCommandEntryCommand>
        {
            private readonly ICommandsEntriesRepository _commandsEntriesRepository;

            public Handler(ICommandsEntriesRepository commandsEntriesRepository)
            {
                _commandsEntriesRepository = commandsEntriesRepository;
            }

            public async Task<Unit> Handle(UpdateCommandEntryCommand request, CancellationToken cancellationToken)
            {
                var model = new CommandEntryDto
                {
                    Name = request.Name,
                    JsonSchema = request.JsonSchema,
                    Id = request.Id
                };

                await _commandsEntriesRepository.Update(model);
                return Unit.Value;
            }
        }
    }
}
