using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Exceptions;
using CommandsRegistry.Application.Users.Commands.CreateUser;
using CommandsRegistry.Application.Users.Commands.UpdateUser;
using CommandsRegistry.Domain.Entities.Core;
using CommandsRegistry.Domain.Enums;

namespace CommandsRegistry.Application.IntegrationTests.Users.Commands
{
    using static Testing;

    public class UpdateUserTests : TestBase
    {
        [Test]
        public void ShouldRequireValidUserId()
        {
            var command = new UpdateUserCommand()
            {
                UserId = new Guid("030B4A82-1B7C-11CF-9D53-00AA003C9CB6"),
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<NotFoundException>()
                .Where(x => x.Message.Contains("user") && x.Message.Contains("was not found"));
        }

        [Test]
        public async Task ShouldUpdateUserName()
        {
            var createCommand = new CreateUserCommand()
            {
                UserName = "demo",
                Email = "demo@test.com",
                FirstName = "demoFirst",
                LastName = "demoLast",
                Password = "!Demo123123",
                Roles = new List<string>() { nameof(UserRoles.Client) }
            };

            var userId = await SendAsync(createCommand);

            var command = new UpdateUserCommand()
            {
                UserId = new Guid(userId),
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
            };

            await SendAsync(command);

            var user = await FindAsync<UserAccount>(userId);

            user.Should().NotBeNull();
            user.FirstName.Should().Be(command.FirstName);
            user.LastName.Should().Be(command.LastName);
        }
    }
}