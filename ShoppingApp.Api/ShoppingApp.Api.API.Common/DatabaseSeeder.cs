using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ShoppingApp.Api.API.Data.Entities;

namespace ShoppingApp.Api.API.Common
{
    public static class DatabaseSeeder
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[] { "Administrator", "User" };

            // Create roles
            foreach (var role in roles)
            {
                // Create Administrator Role
                if (roleManager.FindByNameAsync(role).GetAwaiter().GetResult() == null)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }
        }

        public static void SeedUsers(UserManager<UserEntity> userManager, IConfiguration config)
        {
            var adminUsername = config["Admin:Username"];
            // Create Administrator Power User
            if (userManager.FindByNameAsync(adminUsername).GetAwaiter().GetResult() == null)
            {
                var administratorAccount = new UserEntity()
                {
                    UserName = adminUsername,
                    Email = config["Admin:Email"],
                    PhoneNumber = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                };

                var result = userManager.CreateAsync(administratorAccount, config["Admin:Password"]).GetAwaiter().GetResult();
                if (!result.Succeeded) throw new ApplicationException(result.Errors.First().Description);

                // Add Administrator role to admin user
                var roleResult = userManager.AddToRoleAsync(administratorAccount, "Administrator").GetAwaiter().GetResult();
                if (!roleResult.Succeeded) throw new ApplicationException("Failed to add administrator role to admin account");
            }
        }
    }
}