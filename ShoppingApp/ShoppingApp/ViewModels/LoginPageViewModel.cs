using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ShoppingApp.Application.Contracts;
using ShoppingApp.Application.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShoppingApp.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly IApiService _apiService;
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;

        [DataType(DataType.Password)]
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginPageViewModel(
            IApiService apiService,
            IPageDialogService dialogService,
            INavigationService navigationService)
        {
            _apiService = apiService;
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public DelegateCommand PerformLoginCommand => new DelegateCommand(async () => await LoginAction());

        public async Task LoginAction()
        {
            IsBusy = true;

            try
            {
                await _apiService.RequestSignInAsync(new SignInRequest() { Password = Password, Username = Username });
                await _navigationService.NavigateAsync("/MainPage");
            }
            catch (Exception e)
            {
                await _dialogService.DisplayAlertAsync("Error", e.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}