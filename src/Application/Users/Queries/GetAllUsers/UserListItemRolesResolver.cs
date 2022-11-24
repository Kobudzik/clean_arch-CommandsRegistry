using AutoMapper;
using System.Linq;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Application.Users.Queries.GetAllUsers;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Users.Queries.GetAllUsers
{
    public class UserListItemRolesResolver : IValueResolver<UserAccount, UserListItemModel, string[]>
    {
        private readonly IUserManagementService _userManagementService;

        public UserListItemRolesResolver(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public string[] Resolve(UserAccount source, UserListItemModel destination, string[] destMember, ResolutionContext context)
        {
            var roles = _userManagementService.GetUserRolesAsync(source.UserName)
                .GetAwaiter()
                .GetResult()
                .ToArray();

            return roles;
        }
    }
}
