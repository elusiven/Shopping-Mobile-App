using AutoMapper;
using ShoppingApp.Api.Services;
using ShoppingApp.Api.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Api.API.Data.Contracts;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.API.Data.Repositories;
using ShoppingApp.Api.Services.Services;

namespace ShoppingApp.Api.IoC.Configuration.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services != null)
            {
                services.AddScoped<ICategoryRepository, CategoryRepository>();

                services.AddTransient<IUserService, UserService>();
                services.AddTransient<ICategoryService, CategoryService>();
                services.AddTransient<ILocalFileService, LocalFileService>();
            }
        }

        public static void ConfigureMappings(this IServiceCollection services)
        {
            if (services != null)
            {
                //Automap settings
                services.AddAutoMapper(Assembly.GetExecutingAssembly());
            }
        }
    }
}