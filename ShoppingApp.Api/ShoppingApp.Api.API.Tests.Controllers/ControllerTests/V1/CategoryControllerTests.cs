using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingApp.Api.API.Controllers.V1;
using ShoppingApp.Api.Services.Contracts;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Tests.Controllers.ControllerTests.V1
{
    [TestClass]
    public class CategoryControllerTests : TestBase
    {
        private CategoryController _controller;

        public CategoryControllerTests()
        {
            var businessService = _serviceProvider.GetRequiredService<ICategoryService>();
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<CategoryController>();

            _controller = new CategoryController(businessService, logger);
        }

        [TestMethod]
        public async Task CreateUser_Nominal_OK()
        {
            // Act
            var result = await _controller.GetAll();
            // Assert
            Assert.IsNotNull(result);
        }
    }
}