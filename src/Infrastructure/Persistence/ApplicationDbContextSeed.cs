using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CommandsRegistry.Infrastructure.Identity;

namespace CommandsRegistry.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAndRolesAsync(
            UserManager<UserAccount> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");
            var clientRole = new IdentityRole("Client");

            var roles = roleManager.Roles.Select(s => s.Name).ToList();

            if (roles.All(r => r != administratorRole.Name))
                await roleManager.CreateAsync(administratorRole);

            if (roles.All(r => r != clientRole.Name))
                await roleManager.CreateAsync(clientRole);

            var administrator = UserAccount.Create("Admin", "Admin", "administrator@localhost", "Admin", true);
            var kobudzik = UserAccount.Create("Konrad", "Budzik", "konrad@localhost", "kobudzik", true);
            var kabdzik = UserAccount.Create("Kamil", "Budzik", "kamil@localhost", "kabdzik", true);

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                administrator.EmailConfirmed = true;

                await userManager.CreateAsync(administrator, "zxcasd");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }

            if (userManager.Users.All(u => u.UserName != kobudzik.UserName))
            {
                kobudzik.EmailConfirmed = true;
                await userManager.CreateAsync(kobudzik, "zxcasd");
                await userManager.AddToRolesAsync(kobudzik, new[] { administratorRole.Name });
                await userManager.AddToRolesAsync(kobudzik, new[] { clientRole.Name });
            }

            if (userManager.Users.All(u => u.UserName != kabdzik.UserName))
            {
                kabdzik.EmailConfirmed = true;
                await userManager.CreateAsync(kabdzik, "zxcasd");
                await userManager.AddToRolesAsync(kabdzik, new[] { clientRole.Name });
            }
        }
    }
}
