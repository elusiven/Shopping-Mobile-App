using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingApp.Api.API.Controllers.V1;
using ShoppingApp.Api.Services.Contracts;
using ShoppingApp.Api.Services.Model.User;
using System;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Tests.Controllers.ControllerTests.V1
{
    [TestClass]
    public class UserControllerTests : TestBase
    {
        //NOTE: should be replaced by an interface
        private UserController _controller;

        public UserControllerTests() : base()
        {
            var businessService = _serviceProvider.GetRequiredService<IUserService>();
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<UserController>();

            _controller = new UserController(businessService, mapper, logger);
        }

        [TestMethod]
        public async Task CreateUser_Nominal_OK()
        {
            // Arrange
            CreateUserModel model = new CreateUserModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Testor",
                EmailAddress = "test@me.com"
            };

            // Act
            var result = await _controller.CreateUser(model);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}