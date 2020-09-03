using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ShoppingApp.Api.Services.Contracts
{
    public interface IBaseFileService
    {
        Task<string> SaveImage(IFormFile file, FileMode mode);
    }
}