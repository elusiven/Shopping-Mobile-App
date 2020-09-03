using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Api.Services.Model.Product
{
    public class UpdateCategoryResourceModel
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}