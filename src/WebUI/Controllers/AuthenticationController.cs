using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CommandsRegistry.Application.Authentication.Commands.ConfirmEmail;
using CommandsRegistry.Application.Authentication.Commands.SignIn;
using CommandsRegistry.Infrastructure.Configuration;
using CommandsRegistry.WebUI.DTO.Authentication;
using CommandsRegistry.WebUI.DTO.Errors;

namespace CommandsRegistry.WebUI.Controllers
{
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAplicationConfiguration aplicationConfiguration;

        public AuthenticationController(IAplicationConfiguration aplicationConfiguration)
        {
            this.aplicationConfiguration = aplicationConfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
        {
            var signInCommandResponse = await Mediator.Send(new SignInCommand(request.Username, request.Password));

            var response = new SignInResponse
            {
                Token = signInCommandResponse.AccessToken,
                RefreshToken = signInCommandResponse.RefreshToken,
            };

            if (string.IsNullOrEmpty(signInCommandResponse.Error))
                return Ok(response);

            return new UnauthorizedObjectResult(new ExceptionDto { Message = signInCommandResponse.Error });
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConfirmAccountResponse>> ConfirmEmail([FromQuery] ConfirmAccountRequest request)
        {
            await Mediator.Send(new ConfirmEmailCommand()
            {
                Token = request.Token,
                UserId = request.UserId
            });

            return RedirectPermanent(aplicationConfiguration.FrontendUrl + "/login");
        }

        //
        // [AllowAnonymous]
        // [HttpPost]
        // [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult<ResetPasswordResponse>> ResetPassword([FromBody] ResetPasswordRequest request)
        // {
        //     await _userModule.ExecuteCommandAsync(new ResetPasswordCommand
        //     {
        //         UserId = request.UserId,
        //         NewPassword = request.NewPassword,
        //         Token = request.Token
        //     });
        //
        //     return Ok();
        // }
        //
        // [AllowAnonymous]
        // [HttpPost]
        // public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        // {
        //     await _userModule.ExecuteCommandAsync(new ForgotPasswordCommand
        //     {
        //         Email = request.Email
        //     });
        //
        //     return Ok();
        // }
        //
        // [HttpPost]
        // [Authorize(Policy = PolicyNameKeys.TokenValid)]
        // public async Task<ActionResult<SignOutResponse>> SignOut([FromBody] SignOutRequest request)
        // {
        //     await _userModule.ExecuteCommandAsync(new SignOutCommand());
        //
        //     return Ok(new SignOutResponse());
        // }
        //
        // [AllowAnonymous]
        // public async Task<ActionResult<ExchangeRefreshTokenResponse>> ExchangeRefreshToken(
        //     [FromBody] ExchangeRefreshTokenCommand command)
        // {
        //     return await _userModule.ExecuteCommandAsync(command);
        // }
    }
}