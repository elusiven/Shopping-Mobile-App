using System;
using System.Collections.Generic;

namespace ShoppingApp.Api.API.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}