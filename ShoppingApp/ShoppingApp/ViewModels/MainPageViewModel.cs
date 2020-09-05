using Prism.Navigation;
using ShoppingApp.Application.Models.Product;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ShoppingApp.Application.Contracts;

namespace ShoppingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Category> Categories { get; set; }

        public MainPageViewModel(
            INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = "Main Page";
            Categories = new ObservableCollection<Category>();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Categories = await _apiService.RequestCategoriesAsync();
        }
    }
}