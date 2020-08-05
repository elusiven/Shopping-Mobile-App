using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Api.API.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryEntityId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public byte[] ImageArray { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingCartItemEntity> ShoppingCartItems { get; set; }
    }
}