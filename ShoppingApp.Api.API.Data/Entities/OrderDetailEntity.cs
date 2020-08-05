using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Data.Entities
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int ProductEntityId { get; set; }
        public ProductEntity Product { get; set; }
    }
}