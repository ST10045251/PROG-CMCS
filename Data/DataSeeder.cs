using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ST10045251_PROTOTYPE.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminUserAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //Checking to see if Manager role exists, and create it if it doesn't
            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            //Creating the manager user if it doesn't exist
            var managerUser = await userManager.FindByEmailAsync("manager@yourdomain.com");
            if (managerUser == null)
            {
                managerUser = new IdentityUser
                {
                    UserName = "Manager",
                    Email = "manager@yourdomain.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(managerUser, "ManagerPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(managerUser, "Manager");
                }
            }
        }
    }
}
