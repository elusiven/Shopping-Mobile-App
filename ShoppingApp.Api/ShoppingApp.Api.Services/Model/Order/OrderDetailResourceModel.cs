using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Api.Services.Model.Product;

namespace ShoppingApp.Api.Services.Model.Order
{
    public class OrderDetailResourceModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int OrderId { get; set; }
        public OrderResourceModel Order { get; set; }
        public int ProductEntityId { get; set; }
        public ProductResourceModel Product { get; set; }
    }
}