using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Data.Entities
{
    public class ShoppingCartItemEntity
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductEntityId { get; set; }
        public int CustomerEntityId { get; set; }
    }
}