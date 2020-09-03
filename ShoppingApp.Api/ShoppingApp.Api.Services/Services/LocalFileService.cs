using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Api.Services.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace ShoppingApp.Api.Services.Services
{
    public class LocalFileService : BaseFileService, ILocalFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalFileService(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            var uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
        }

        public override async Task<string> SaveImage(IFormFile file, FileMode mode)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads", file.FileName);

            if (file != null && file.Length > 0)
            {
                await using var fileStream = new FileStream(path, mode);
                await file.CopyToAsync(fileStream);
            }

            var returnPath = Path.Combine("uploads", file.FileName);

            return File.Exists(path) ? returnPath : string.Empty;
        }
    }
}