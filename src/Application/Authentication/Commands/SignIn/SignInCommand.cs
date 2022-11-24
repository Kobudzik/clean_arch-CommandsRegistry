using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using MediatR;

namespace CommandsRegistry.Application.Authentication.Commands.SignIn
{
    public sealed class SignInCommand : IRequest<SignInCommandResponse>
    {
        public SignInCommand(string userName, string password)
        {
            Username = userName;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }

        internal sealed class Handler : IRequestHandler<SignInCommand, SignInCommandResponse>
        {
            private readonly IJwtTokenManagementService jwtTokenManagementService;
            private readonly IRefreshTokenGenerationService refreshTokenGenerationService;
            private readonly ISignInManagementService signInManagementService;
            private readonly IUserManagementService userManagementService;

            public Handler(ISignInManagementService signInManagementService,
                IJwtTokenManagementService jwtTokenManagementService,
                IUserManagementService userManagementService,
                IRefreshTokenGenerationService refreshTokenGenerationService)
            {
                this.signInManagementService = signInManagementService;
                this.jwtTokenManagementService = jwtTokenManagementService;
                this.userManagementService = userManagementService;
                this.refreshTokenGenerationService = refreshTokenGenerationService;
            }

            public async Task<SignInCommandResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                var signInResultStatus = await signInManagementService.SignInAsync(request.Username, request.Password);

                if (signInResultStatus == SignInResultStatus.Failure)
                {
                    //await signInManagementService.SaveLogForFailedLoginAttemptAsync(request.Username, cancellationToken);

                    var signInFailedCommandResponse = SignInCommandResponse.CreateFailed(signInResultStatus);
                    return signInFailedCommandResponse;
                }

                var userRole = await userManagementService.GetUserRolesAsync(request.Username);

                var userDetails = await userManagementService.GetUserDetailsAsync(request.Username);
                var jwtToken = jwtTokenManagementService.GenerateJwtToken(userDetails, userRole);
                var refreshToken = await refreshTokenGenerationService.GenerateRefreshTokenAsync(userDetails.Id);

                var signInCommandResponse = SignInCommandResponse.CreateSuccess(jwtToken, refreshToken);
                return signInCommandResponse;
            }
        }
    }
}
