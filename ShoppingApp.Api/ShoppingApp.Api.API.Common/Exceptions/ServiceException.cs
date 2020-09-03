using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Common.Exceptions
{
    public class ServiceException : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ServiceException(string message, Exception innerEx) : base(message, innerEx)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ServiceException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ServiceException()
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}