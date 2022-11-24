using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using MediatR;

namespace CommandsRegistry.Application.JsonCommands.Commands.DeleteCommandEntry
{
    public class DeleteCommandEntryCommand : IRequest
    {
        public DeleteCommandEntryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }

        internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteCommandEntryCommand>
        {
            private readonly ICommandsEntriesRepository _commandsEntriesRepository;

            public DeleteUserCommandHandler(ICommandsEntriesRepository commandsEntriesRepository)
            {
                _commandsEntriesRepository = commandsEntriesRepository;
            }

            public async Task<Unit> Handle(DeleteCommandEntryCommand request, CancellationToken cancellationToken)
            {
                await _commandsEntriesRepository.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}