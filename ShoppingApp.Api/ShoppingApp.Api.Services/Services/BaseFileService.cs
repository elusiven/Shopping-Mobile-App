using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Api.Services.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace ShoppingApp.Api.Services.Services
{
    public abstract class BaseFileService : IBaseFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        protected BaseFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public abstract Task<string> SaveImage(IFormFile file, FileMode mode);
    }
}