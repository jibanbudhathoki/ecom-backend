using Ecom.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Ecom.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ApplicationDbContextSeed");
            
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                var adminRole = new ApplicationRole { Name = "Admin" };
                if (await roleManager.FindByNameAsync(adminRole.Name!) == null)
                {
                    await roleManager.CreateAsync(adminRole);
                }

                var defaultAdmin = new ApplicationUser
                {
                    UserName = "admin@ecom.com",
                    Email = "admin@ecom.com",
                    EmailConfirmed = true
                };

                if (await userManager.FindByEmailAsync(defaultAdmin.Email!) == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "Admin123!");
                    await userManager.AddToRoleAsync(defaultAdmin, adminRole.Name!);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while seeding the database. Make sure PostgreSQL is running.");
            }
        }
    }
}
