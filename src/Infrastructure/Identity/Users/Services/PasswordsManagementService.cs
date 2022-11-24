using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Infrastructure.Identity.Users.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommandsRegistry.Infrastructure.Identity.Users.Services
{
    internal sealed class PasswordsManagementService : IPasswordsManagementService
    {
        private readonly UserManager<UserAccount> _applicationUserManager;

        public PasswordsManagementService(UserManager<UserAccount> applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public async Task<string> GenerateResetUserPasswordTokenAsync(Guid userGuid)
        {
            var user = await _applicationUserManager.Users
                .AsNoTracking()
                .SingleAsync(u => u.Id == userGuid.ToString());

            return await _applicationUserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateConfirmAccountTokenAsync(Guid userGuid)
        {
            var user = await _applicationUserManager.Users
                .AsNoTracking()
                .SingleAsync(u => u.Id == userGuid.ToString());

            return await _applicationUserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ForgotPasswordAsync(Guid userGuid)
        {
            var user = await _applicationUserManager.Users
                .AsNoTracking()
                .SingleAsync(u => u.Id == userGuid.ToString());

            if (!user.EmailConfirmed)
                throw new InvalidOperationException("User email is not confirmed");

            var resetPasswordToken = await _applicationUserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(Guid userGuid, string newPassword, string resetPasswordToken, CancellationToken cancellationToken = default)
        {
            var user = await _applicationUserManager.Users
                .AsNoTracking()
                .SingleAsync(u => u.Id == userGuid.ToString(), cancellationToken);

            var result = await _applicationUserManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);

            if (result.Succeeded)
                return result.Succeeded;

            var tokenExpiredError = result.Errors.SingleOrDefault(c => c.Code == IndentityErrorsCodes.TokenExpired);

            if (tokenExpiredError != null)
                throw new InvalidOperationException("Token Expired");

            var passwordRepeatedError =
                result.Errors.SingleOrDefault(c => c.Code == IndentityErrorsCodes.PasswordRepeated);

            if (passwordRepeatedError != null)
                throw new InvalidOperationException("Password Reapted");

            throw new InvalidOperationException($"Error during reset password for user: {user}. Message: {result.Errors}");
        }

        public async Task<bool> ChangeUserPasswordAsync(Guid userGuid, string newPassword, CancellationToken cancellationToken = default)
        {
            var user = await _applicationUserManager.Users
                .SingleAsync(userAccount => userAccount.Id == userGuid.ToString(), cancellationToken);

            if (user == null)
                throw new InvalidOperationException("User is not found");

            var token = await _applicationUserManager.GeneratePasswordResetTokenAsync(user);
            var result = await _applicationUserManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded) return result.Succeeded;

            var passwordRepeatedError = result.Errors.SingleOrDefault(c => c.Code == IndentityErrorsCodes.PasswordRepeated);

            if (passwordRepeatedError != null)
                throw new InvalidOperationException(passwordRepeatedError.Description, null);

            var errorMessages = result.Errors.Select(error => error.Description).ToArray();

            throw new InvalidOperationException(
                $"Error during resetting password for user: {user}. Message: {string.Join(",", errorMessages)}");
        }

        public async Task ConfirmUserEmailAndSetPasswordAsync(Guid userGuid, string token, string password)
        {
            var user = await _applicationUserManager.Users.SingleAsync(u => u.Id == userGuid.ToString());

            if (user.EmailConfirmed)
                throw new InvalidOperationException("Email is already confirmed");

            var confirmEmailResult = await _applicationUserManager.ConfirmEmailAsync(user, token);

            if (!confirmEmailResult.Succeeded)
                throw new InvalidOperationException("Link Expired");

            var addPasswordResult = await _applicationUserManager.AddPasswordAsync(user, password);

            if (!addPasswordResult.Succeeded)
                throw new InvalidOperationException("Setting password not succeeded: " + addPasswordResult);
        }
    }
}