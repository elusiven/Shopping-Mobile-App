using System.Collections.Generic;

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