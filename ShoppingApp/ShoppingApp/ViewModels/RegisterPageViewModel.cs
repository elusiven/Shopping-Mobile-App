using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ShoppingApp.Application.Contracts;
using ShoppingApp.Application.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Common.Exceptions;
using ShoppingApp.Validations;

namespace ShoppingApp.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IPageDialogService _pageDialogService;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private ValidatableObject<string> _firstName;

        public ValidatableObject<string> FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand NavigateToLoginPageCommand => new DelegateCommand(
            async () => await _navigationService.NavigateAsync("LoginPage"));

        public DelegateCommand PerformSignUpCommand => new DelegateCommand(async () => await SignUpAction());

        private async Task SignUpAction()
        {
            IsBusy = true;

            try
            {
                await _apiService.RequestSignUpAsync(new SignUpRequest()
                {
                    FirstName = FirstName.Value,
                    EmailAddress = EmailAddress,
                    Password = Password
                });
                await _apiService.RequestSignInAsync(new SignInRequest()
                { Username = EmailAddress, Password = Password });
                await _navigationService.NavigateAsync("/NavigationPage/MainPage");
            }
            catch (ApiServiceException ex)
            {
                if (ex.Errors != null && ex.Errors.Count > 0)
                {
                    var firstError = ex.Errors.FirstOrDefault().Value[0];
                    await _pageDialogService.DisplayAlertAsync("Error", firstError, "Ok");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public RegisterPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _pageDialogService = pageDialogService;

            FirstName = new ValidatableObject<string>();
        }
    }
}