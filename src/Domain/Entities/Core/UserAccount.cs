using System;
using Microsoft.AspNetCore.Identity;

namespace CommandsRegistry.Domain.Entities.Core
{
    public class UserAccount : IdentityUser
    {
        public UserAccount()
        {
        }

        private UserAccount(string firstName, string lastName, string email, string username, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = username;
            IsActive = isActive;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Guid => Guid.Parse(Id);
        public string ThemePrimaryColor { get; private set; }

        public static UserAccount Create(string firstName, string lastName, string email, string username, bool isActive)
            => new (firstName, lastName, email, username, isActive);

        public override string ToString()
        {
            var name = $"{FirstName} {LastName}".Trim();
            return String.IsNullOrEmpty(name) ? UserName : name;
        }
    }
}
