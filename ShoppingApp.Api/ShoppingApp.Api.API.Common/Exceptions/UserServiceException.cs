using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Common.Exceptions
{
    public class UserServiceException : ServiceException
    {
        public UserServiceException(string message) : base(message)
        {
        }

        public UserServiceException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}