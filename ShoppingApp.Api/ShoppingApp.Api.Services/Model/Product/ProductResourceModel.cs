using Newtonsoft.Json;
using ShoppingApp.Api.API.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingApp.Api.Services.Model.Order;

namespace ShoppingApp.Api.Services.Model.Product
{
    public class ProductResourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public byte[] ImageArray { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetailResourceModel> OrderDetails { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingCartItemEntity> ShoppingCartItems { get; set; }
    }
}