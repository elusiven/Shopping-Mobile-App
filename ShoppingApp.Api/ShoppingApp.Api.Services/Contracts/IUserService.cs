using ShoppingApp.Api.Services.Model;
using System.Threading.Tasks;
using ShoppingApp.Api.Services.Model.User;

namespace ShoppingApp.Api.Services.Contracts
{
    public interface IUserService
    {
        Task<UserResourceModel> CreateAsync(CreateUserModel user);

        Task<UserResourceModel> UpdateAsync(UpdateUserModel user);

        Task<bool> DeleteAsync(string id);

        Task<UserResourceModel> GetAsync(string id);
    }
}