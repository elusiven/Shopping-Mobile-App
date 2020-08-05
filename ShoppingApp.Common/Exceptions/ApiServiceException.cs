using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingApp.Common.Exceptions
{
    public class ApiServiceException : ServiceException
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ApiServiceException(string message, Exception innerException) : base(message, innerException)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ApiServiceException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}