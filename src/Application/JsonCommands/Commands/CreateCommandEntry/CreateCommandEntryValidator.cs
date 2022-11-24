using FluentValidation;

namespace CommandsRegistry.Application.JsonCommands.Commands.CreateCommandEntry
{
    public class CreateCommandEntryValidator : AbstractValidator<CreateCommandEntryCommand>
    {
        public CreateCommandEntryValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.JsonSchema).MaximumLength(8000);
            RuleFor(x => x.Name).MaximumLength(255).NotEmpty();
        }
    }
}