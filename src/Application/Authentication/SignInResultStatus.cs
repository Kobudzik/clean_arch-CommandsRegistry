namespace CommandsRegistry.Application.Authentication
{
    public enum SignInResultStatus
    {
        Success = 1,
        Failure = 2,
        ExpiredPassword = 3,
        Locked = 4
    }
}