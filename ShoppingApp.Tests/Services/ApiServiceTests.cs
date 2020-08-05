using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingApp.Application.Contracts;
using ShoppingApp.Application.Models.Requests;
using ShoppingApp.Application.Services;
using ShoppingApp.Common.Exceptions;

namespace ShoppingApp.Tests.Services
{
    [TestClass]
    public class ApiServiceTests : BaseTest
    {
        [TestMethod]
        public async Task Test_Fail_SignIn()
        {
            // Arrange
            SignInRequest requestModel = new SignInRequest
            {
                Username = "Administrator",
                Password = ""
            };

            // Act
            await Assert.ThrowsExceptionAsync<ApiServiceException>(async () =>
                await ApiService.RequestSignInAsync(requestModel));

            // Assert
            Assert.IsNotNull(ApiService);
            Assert.IsNotNull(requestModel);
        }
    }
}