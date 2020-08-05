using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingApp.Application.Contracts;
using ShoppingApp.Application.Services;

namespace ShoppingApp.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected IApiService ApiService;
        protected IStorageService StorageService;

        [TestInitialize]
        public void Init()
        {
            if (StorageService == null) StorageService = new StorageService();
            if (ApiService == null) ApiService = new ApiService(StorageService);
        }
    }
}