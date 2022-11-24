using FluentValidation;

namespace CommandsRegistry.Application.JsonCommands.Commands.DeleteCommandEntry
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteCommandEntryCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}