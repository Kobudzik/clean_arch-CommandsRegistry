using FluentValidation;
using CommandsRegistry.Application.Common.Extensions.Validations;

namespace CommandsRegistry.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FirstName).MaximumLength(255).NotEmpty().MustNotFakeAdmin();
            RuleFor(x => x.LastName).MaximumLength(255).NotEmpty().MustNotFakeAdmin();
            RuleFor(x => x.UserName).Username().NotEmpty().MustNotFakeAdmin();
            RuleFor(x => x.Email).MaximumLength(255).EmailAddress().NotEmpty().MustNotFakeAdmin();
            RuleFor(x => x.Password).MaximumLength(255).NotEmpty().MustBeCorrectPasswordFormat();
            //RuleFor(x => x.Roles).NotEmpty();
        }
    }
}