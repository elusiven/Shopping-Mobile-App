using System.Collections.Generic;

namespace ShoppingApp.Api.Services.Model.Product
{
    public class CategoryResourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<ProductResourceModel> Products { get; set; }
    }
}