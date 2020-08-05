using System;
using System.Linq;
using Newtonsoft.Json;
using ShoppingApp.Application.Models.User;

namespace ShoppingApp.Application.Models.Requests
{
    public class SignUpRequest
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName
        {
            get
            {
                var secondPart = FirstName?.Split(' ')[1];
                return !string.IsNullOrEmpty(secondPart) ? secondPart : string.Empty;
            }
        }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}