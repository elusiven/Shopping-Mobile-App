using Prism.Navigation;
using ShoppingApp.Application.Models.Product;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using ShoppingApp.Application.Contracts;

namespace ShoppingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public DelegateCommand LogOutCommand => new DelegateCommand(() =>
        {
            _apiService.SignOut();
            _navigationService.NavigateAsync("/LoginPage");
        });

        public MainPageViewModel(
            INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Main Page";
            Categories = new ObservableCollection<Category>();

            Categories.Add(new Category
            {
                Id = 42,
                ImageUrl = "https://google.com",
                Name = "Snacks"
            });
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Categories = await _apiService.RequestCategoriesAsync();
        }
    }
}