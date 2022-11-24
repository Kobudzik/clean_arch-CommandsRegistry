using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsRegistry.Application.Authentication.DTOs;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.Users.Commands.CreateUser.Request;
using CommandsRegistry.Application.Users.Commands.UpdateUser.Request;
using CommandsRegistry.Application.Users.Queries.GetAllUsers;
using CommandsRegistry.Application.Users.Queries.GetUserSettings;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Common.Interfaces.User
{
    public interface IUserManagementService
    {
        Task<UserAccount> CreateUserAsync(CreateUserModel createUserRequest);
        Task UpdateAsync(UpdateUserModel updateUserDto);
        Task SetAssignedRolesAsync(Guid userPublicId, List<string> newRolesNames);
        Task<bool> UserExists(string userName);
        Task AddUserToRoleAsync(string userName, string roleName);
        Task AddUserToRolesAsync(Guid userPublicId, List<string> rolesNames);
        Task<UserDto> GetUserDetailsAsync(string userName);
        Task<UserDto> GetUserDetailsAsync(Guid userPublicId);
        Task<List<UserListItemModel>> GetUsersAsync(Pager pager = null, GetPaginatedUsersFilterModel filter = null);
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<IList<string>> GetUserRolesAsync(Guid userPublicId);
        Task<bool> RemoveUserAsync(string userPublicId);
        Task<SettingsVM> GetUserSettingsAsync(Guid userId);

        Task ConfirmEmailAsync(string userId, string token);
        Task<string> GenerateEmailConfirmationToken(UserAccount userAccount);

        Task<bool> GetRoleAsync(string roleName);
    }
}