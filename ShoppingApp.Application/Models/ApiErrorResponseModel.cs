using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingApp.Application.Models
{
    public class ApiErrorResponseModel
    {
        public ApiErrorResponseModel()
        {
            Errors = new Dictionary<string, string[]>();
        }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("errors")]
        public Dictionary<string, string[]> Errors { get; set; }

        public bool IsSuccess => Status < 300 && Status >= 200;
    }
}