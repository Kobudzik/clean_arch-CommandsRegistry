using System.Threading.Tasks;

namespace CommandsRegistry.Application.Authentication
{
    public interface IRoleManagementService
    {
        Task AddNewRole(string roleName);
        Task<bool> RoleExists(string roleName);
    }
}