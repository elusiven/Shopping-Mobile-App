using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Application.Models;
using ShoppingApp.Application.Models.Requests;

namespace ShoppingApp.Application.Contracts
{
    public interface IApiService
    {
        string BaseUrl { get; }

        Task RequestSignInAsync(SignInRequest model);

        Task RequestSignUpAsync(SignUpRequest model);
    }
}