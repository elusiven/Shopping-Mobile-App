using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Api.Services.Model.User
{
    public class CreateUserModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(length: 100, ErrorMessage = "First name has to be less than 100 characters long")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(length: 100, ErrorMessage = "Last name has to be less than 100 characters long")]
        public string LastName { get; set; }

        public AddressModel Address { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(length: 100, ErrorMessage = "Email address has to be less than 100 characters long")]
        [MinLength(length: 5, ErrorMessage = "Email address has to be at least 5 characters long")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(length: 8, ErrorMessage = "Password has to be at least 8 characters long")]
        [MaxLength(length: 100, ErrorMessage = "Password has to be less than 100 characters long")]
        public string Password { get; set; }
    }
}