using System;

namespace CommandsRegistry.Infrastructure.Identity.Users.Exceptions
{
    [Serializable]
    public class UserNotDeletedException : Exception
    {
        public UserNotDeletedException()
            : base("Unable to remove user: not found")
        {
        }
    }
}