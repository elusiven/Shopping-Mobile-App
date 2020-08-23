using Newtonsoft.Json;
using ShoppingApp.Application.Contracts;
using ShoppingApp.Application.Models;
using ShoppingApp.Application.Models.Requests;
using ShoppingApp.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly IStorageService _storageService;
        private string _bearerToken;

        public ApiService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public string BaseUrl => "https://192.168.43.197:45455/";

        /// <summary>
        /// Request bearer token payload from the API
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A boolean value indicating whether request was successful</returns>
        public async Task RequestSignInAsync(SignInRequest model)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseUrl);

                    FormUrlEncodedContent parameters = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", model.Username),
                        new KeyValuePair<string, string>("password", model.Password),
                        new KeyValuePair<string, string>("scope", "app.api.shopping.full"),
                        new KeyValuePair<string, string>("client_id", "t8agr5xKt4$3"),
                        new KeyValuePair<string, string>("client_secret", "eb300de4-add9-42f4-a3ac-abd3c60f1919")
                    });

                    var response = await httpClient.PostAsync("connect/token", parameters);

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenPayload =
                            JsonConvert.DeserializeObject<TokenPayloadModel>(await response.Content.ReadAsStringAsync());
                        await _storageService.SetObjectInSecureStorageAsync(Storage.Keys.TokenPayload, tokenPayload);
                        _bearerToken = tokenPayload.AccessToken;
                    }
                    else
                    {
                        throw new ApiServiceException("Invalid Credentials.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException?.GetType() == typeof(SocketException))
                    throw new ApiServiceException("Internet connection is required.", ex);

                throw new ApiServiceException("Failed to authenticate user.", ex);
            }
        }

        /// <summary>
        /// Request sign up action from the API (User Creation)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RequestSignUpAsync(SignUpRequest model)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseUrl);

                    var response = await httpClient.PostAsync("api/v1/users",
                        new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                    if (!response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponseModel = JsonConvert.DeserializeObject<ApiErrorResponseModel>(responseContent);
                        throw new ApiServiceException("One or more validation errors occurred") { Errors = errorResponseModel?.Errors };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException?.GetType() == typeof(SocketException))
                    throw new ApiServiceException("Internet connection is required.", ex);

                throw new ApiServiceException("Failed to register user.", ex);
            }
        }
    }
}