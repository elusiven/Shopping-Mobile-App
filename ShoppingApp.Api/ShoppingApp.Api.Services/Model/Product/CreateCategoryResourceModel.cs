using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Api.Services.Model.Product
{
    public class CreateCategoryResourceModel
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}