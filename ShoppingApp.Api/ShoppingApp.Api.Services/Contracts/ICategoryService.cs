using System.Collections.Generic;
using ShoppingApp.Api.Services.Model.Product;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Api.Services.Contracts
{
    public interface ICategoryService
    {
        Task<CategoryResourceModel> GetById(int id);

        Task<List<CategoryResourceModel>> GetAll();

        Task<CategoryResourceModel> Create(CreateCategoryResourceModel model);

        Task<CategoryResourceModel> Update(UpdateCategoryResourceModel model);

        Task Delete(int id);
    }
}