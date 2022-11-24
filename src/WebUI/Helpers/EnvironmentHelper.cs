namespace CommandsRegistry.WebUI.Helpers
{
    public static class EnvironmentHelper
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
                return false;
#endif
        }
    }
}
