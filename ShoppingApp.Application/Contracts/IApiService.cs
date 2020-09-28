using ShoppingApp.Application.Models.Product;
using ShoppingApp.Application.Models.Requests;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShoppingApp.Application.Contracts
{
    public interface IApiService
    {
        string BaseUrl { get; }

        Task RequestSignInAsync(SignInRequest model);

        Task RequestSignUpAsync(SignUpRequest model);

        Task<ObservableCollection<Category>> RequestCategoriesAsync();

        void SignOut();
    }
}