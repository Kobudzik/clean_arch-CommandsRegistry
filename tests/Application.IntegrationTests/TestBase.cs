using NUnit.Framework;
using System.Threading.Tasks;

namespace CommandsRegistry.Application.IntegrationTests
{
    using static Testing;

    /// <summary>
    /// Resets state before each test
    /// </summary>
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
