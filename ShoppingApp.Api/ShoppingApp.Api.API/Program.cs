using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ShoppingApp.Api.API.Common.Extensions;

#pragma warning disable CS1591

namespace ShoppingApp.Api.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .SeedData()
                .Run();
        }

        //.NET CORE 3.0
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}