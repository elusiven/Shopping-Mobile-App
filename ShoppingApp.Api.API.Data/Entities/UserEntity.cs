using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Api.API.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressEntity Address { get; set; }
    }
}