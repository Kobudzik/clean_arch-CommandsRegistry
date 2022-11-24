using System;

namespace CommandsRegistry.Infrastructure.Identity.Users.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found")
        {
        }
    }
}