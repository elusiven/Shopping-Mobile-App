namespace ShoppingApp.Api.Services.Model.User
{
    public class UserResourceModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public AddressModel Address { get; set; }
        public string EmailAddress { get; set; }
    }
}