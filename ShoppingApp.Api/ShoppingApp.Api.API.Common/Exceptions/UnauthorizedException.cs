using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Common.Exceptions
{
    public class UnauthorizedException : ServiceException
    {
        public UnauthorizedException(string message, Exception innerEx) : base(message, innerEx)
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}