using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Common
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public Dictionary<string, string[]> Errors { get; set; }
        public int Status { get; set; }
    }
}