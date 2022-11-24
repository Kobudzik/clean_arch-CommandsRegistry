using System.Threading.Tasks;

namespace CommandsRegistry.Infrastructure.Identity.Jwt.RefreshTokens
{
    public interface ITokenStoreManager
    {
        Task<bool> IsCurrentTokenActiveAsync();
        Task DeactivateCurrentAsync();
    }
}