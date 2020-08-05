using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingApp.Api.API.Data;
using ShoppingApp.Api.API.Data.Entities;
using System;
using Microsoft.AspNetCore.Hosting;

namespace ShoppingApp.Api.API.Common.Extensions
{
    public static class HostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ShoppingDbContext context = services.GetService<ShoppingDbContext>();
                UserManager<UserEntity> userManager = services.GetService<UserManager<UserEntity>>();
                RoleManager<IdentityRole> roleManager = services.GetService<RoleManager<IdentityRole>>();
                IConfiguration config = services.GetService<IConfiguration>();

                DatabaseSeeder.SeedRoles(roleManager);
                DatabaseSeeder.SeedUsers(userManager, config);
            }
            return host;
        }
    }
}