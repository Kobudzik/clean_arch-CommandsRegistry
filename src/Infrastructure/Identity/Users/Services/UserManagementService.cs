using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsRegistry.Application.Authentication.DTOs;
using CommandsRegistry.Application.Common.Exceptions;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.Users.Commands.CreateUser.Request;
using CommandsRegistry.Application.Users.Commands.UpdateUser.Request;
using CommandsRegistry.Application.Users.Queries.GetAllUsers;
using CommandsRegistry.Application.Users.Queries.GetUserSettings;
using CommandsRegistry.Domain.Entities.Core;
using CommandsRegistry.Infrastructure.Identity.Users.Errors;
using CommandsRegistry.Infrastructure.Identity.Users.Exceptions;
using CommandsRegistry.Infrastructure.Persistence;
using LinqKit;

namespace CommandsRegistry.Infrastructure.Identity.Users.Services
{
    internal sealed class UserManagementService : IUserManagementService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserManagementService(
            UserManager<UserAccount> userManager,
            IMapper mapper, ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _roleManager = roleManager;
        }

        #region create, update
        public async Task<UserAccount> CreateUserAsync(CreateUserModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
                throw new CustomValidationException(nameof(request.Email), "Email already taken!");

            var applicationUser = UserAccount.Create(request.FirstName, request.LastName, request.Email, request.Username, request.IsActive);

            var result = await _userManager.CreateAsync(applicationUser, request.Password);

            if (result.Succeeded)
                return applicationUser;

            var errors = result.Errors
                .Select(identityError => identityError.Code)
                .ToList();

            if (errors.Contains(IndentityErrorsCodes.UsernameAlreadyTaken) || errors.Contains(IndentityErrorsCodes.DuplicateUserName))
                throw new CustomValidationException(nameof(request.Username), "Username already taken!");

            throw new UserNotCreatedException(errors);
        }

        public async Task UpdateAsync(UpdateUserModel updateUserDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == updateUserDto.UserId.ToString());

            if (user == null)
                throw new NotFoundException(nameof(user), updateUserDto.UserId);

            _mapper.Map(updateUserDto, user);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return;

            var errors = result.Errors
                .Select(x => x.Code)
                .ToList();

            throw new UserNotUpdatedException(errors);
        }

        public async Task AddUserToRoleAsync(string userName, string roleName)
        {
            var user = await _userManager.Users
                .SingleAsync(u => u.UserName == userName);

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task AddUserToRolesAsync(Guid userId, List<string> rolesNames)
        {
            var user = await _userManager.Users.
                SingleAsync(u => u.Id == userId.ToString());

            await _userManager.AddToRolesAsync(user, rolesNames);
        }

        public async Task SetAssignedRolesAsync(Guid userId, List<string> newRolesNames)
        {
            var user = await _userManager.Users
                .SingleAsync(u => u.Id == userId.ToString());

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, newRolesNames);
        }
        #endregion

        #region Get
        public async Task<SettingsVM> GetUserSettingsAsync(Guid userId)
        {
            var user = await _userManager.Users
                .Where(p => p.Id == userId.ToString())
                .SingleOrDefaultAsync();

            if (user == null)
                throw new UserNotFoundException();

            return _mapper.Map<SettingsVM>(user);
        }
        public async Task<UserAccount> GetUserAsync(Guid userId)
        {
            var user = await _userManager.Users
                .SingleAsync(u => u.Id == userId.ToString());

            if (user == null)
                throw new UserNotFoundException();

            return user;
        }

        public async Task<UserDto> GetUserDetailsAsync(Guid userId)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId.ToString());

            if (user == null)
                throw new UserNotFoundException();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserDetailsAsync(string userName)
        {
            var user = await _userManager.Users
                 .SingleAsync(u => u.UserName == userName);

            if (user == null)
                throw new UserNotFoundException();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserListItemModel>> GetUsersAsync(Pager pager = null, GetPaginatedUsersFilterModel filter = null)
        {
            var predicate = PredicateBuilder.New<UserAccount>(true);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.FirstName))
                    predicate.And(x => x.FirstName.Contains(filter.FirstName));

                if (!string.IsNullOrEmpty(filter.LastName))
                    predicate.And(x => x.LastName.Contains(filter.LastName));

                if (!string.IsNullOrEmpty(filter.Username))
                    predicate.And(x => x.UserName.Contains(filter.Username));
            }

            var users = new List<UserAccount>();

            if (pager != null)
            {
                users = await _userManager.Users
                .Where(predicate)
                .Paginate(pager)
                .ToListAsync();
            }
            else
            {
                users = await _userManager.Users
                    .Where(predicate)
                    .ToListAsync();
            }

            return _mapper.Map<List<UserListItemModel>>(users);
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            var user = await _userManager.Users.SingleAsync(u => u.UserName == userName);
            return await GetUserRolesAsync(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Id == userId.ToString());
            return await GetUserRolesAsync(user);
        }

        private async Task<IList<string>> GetUserRolesAsync(UserAccount userAccount)
        {
            return await _userManager.GetRolesAsync(userAccount);
        }
        #endregion

        public async Task<bool> UserExists(string userName)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(u => u.UserName == userName);
            return user != null;
        }

        public async Task<string> GenerateEmailConfirmationToken(UserAccount userAccount)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userAccount);
        }

        public async Task<bool> RemoveUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(p => p.Id == userId);

            if (user == null)
                throw new UserNotDeletedException();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return true;

            throw new ApplicationException("User deletion not handled exception.");
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException();

            await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<bool> GetRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null;
        }
    }
}