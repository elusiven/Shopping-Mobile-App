using ShoppingApp.Api.Services.Model.User;
using System;

namespace ShoppingApp.Api.API.DataContracts.Requests
{
    public class UserCreationRequest
    {
        public DateTime Date { get; set; }

        public UserResourceModel User { get; set; }
    }
}