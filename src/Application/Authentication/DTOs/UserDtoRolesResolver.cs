using AutoMapper;
using System.Linq;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Authentication.DTOs
{
    public class UserDtoRolesResolver : IValueResolver<UserAccount, UserDto, string[]>
    {
        private readonly IUserManagementService _userManagementService;

        public UserDtoRolesResolver(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public string[] Resolve(UserAccount source, UserDto destination, string[] destMember, ResolutionContext context)
        {
            var roles = _userManagementService.GetUserRolesAsync(source.UserName)
                .GetAwaiter()
                .GetResult()
                .ToArray();

            return roles;
        }
    }
}
