using FluentValidation;

namespace CommandsRegistry.Application.Users.Queries.GetUser
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.PublicId).NotEmpty();
        }
    }
}