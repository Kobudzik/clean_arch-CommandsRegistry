using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Exceptions;
using CommandsRegistry.Application.Users.Commands.CreateUser;
using CommandsRegistry.Domain.Enums;
using CommandsRegistry.Infrastructure.Identity;

namespace CommandsRegistry.Application.IntegrationTests.Places.Queries
{
    using static Testing;

    internal class CreateUserTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }

        public async Task ShouldCreateUser()
        {
            var command = new CreateUserCommand()
            {
                UserName = "demo",
                Email = "demo@test.com",
                FirstName = "demoFirst",
                LastName = "demoLast",
                Password = "!Demo123123",
                Roles = new List<string>() { nameof(UserRoles.Client) }
            };

            var userId = await SendAsync(command);

            var user = await FindAsync<UserAccount>(userId);

            user.Should().NotBeNull();
            user.UserName.Should().Be("demo");
            user.Email.Should().Be("demo@test.com");
            user.FirstName.Should().Be("demoFirst");
            user.LastName.Should().Be("demoFirst");
        }

        [Test]
        public async Task ShouldRequireUniqueUser()
        {
            var command = new CreateUserCommand()
            {
                UserName = "demo",
                Email = "demo@test.com",
                FirstName = "demo",
                LastName = "demo",
                Password = "!Demo123123",
                Roles = new List<string>() { nameof(UserRoles.Client) }
            };

            await SendAsync(command);

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<CustomValidationException>();
        }
    }
}