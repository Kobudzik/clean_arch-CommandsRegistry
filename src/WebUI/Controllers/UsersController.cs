using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommandsRegistry.Application.Authentication.DTOs;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.Users.Commands.ChangeUserPassword;
using CommandsRegistry.Application.Users.Commands.CreateUser;
using CommandsRegistry.Application.Users.Commands.DeleteUser;
using CommandsRegistry.Application.Users.Commands.UpdateUser;
using CommandsRegistry.Application.Users.Queries.GetAllUsers;
using CommandsRegistry.Application.Users.Queries.GetUser;
using CommandsRegistry.Application.Users.Queries.GetUserSettings;
using CommandsRegistry.Domain.Enums;

namespace CommandsRegistry.WebUI.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        private ICurrentUserService _currentUser { get; }

        public UsersController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PaginatedList<UserListItemModel>>> GetList(
            [FromQuery] Pager pager,
            [FromQuery] GetPaginatedUsersFilterModel filter)
        {
            var query = new GetPaginatedUsersQuery(pager, filter);
            var usersList = await Mediator.Send(query);

            return usersList;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Get([FromRoute] Guid id)
        {
            if (!_currentUser.IsAdmin && id != _currentUser.UserGuid())
                return Forbid();

            var query = new GetUserDetailQuery { PublicId = id };
            var result = await Mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("settings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettingsVM>> GetSettings()
        {
            var query = new GetUserSettingsQuery { PublicId = _currentUser.UserGuid() };
            var result = await Mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (_currentUser.UserId == null)
                command.Roles = new List<string>() { nameof(UserRoles.Client) };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
        {
            if (!_currentUser.IsAdmin && command.UserId != _currentUser.UserGuid())
                return Forbid();

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid publicId)
        {
            if (!_currentUser.IsAdmin && publicId != _currentUser.UserGuid())
                return Forbid();

            await Mediator.Send(new DeleteUserCommand(publicId));

            return Ok();
        }

        [HttpPut("ChangeUserPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!_currentUser.IsAdmin && command.PublicId != _currentUser.UserGuid())
                return Forbid();

            await Mediator.Send(command);

            return Ok();
        }
    }
}
