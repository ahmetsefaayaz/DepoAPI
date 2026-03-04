using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DepoAPI.Infrastructure.SeedData;

public static class SeedData
{
    public static async Task SeedRoles(RoleManager<Role>roleManager)
    {
        string[] roles = new[] { "Admin", "User" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role()
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };
                await roleManager.CreateAsync(role);
            }
        }

    }
    public static async Task SeedAdmin(UserManager<User> userManager)
    {
        var admin = await userManager.FindByEmailAsync("admin@gmail.com");
        if (admin is null)
        {
            var user = new User { UserName = "Admin", Email = "admin@gmail.com" };
            await userManager.CreateAsync(user, "_AdminPassword0");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}