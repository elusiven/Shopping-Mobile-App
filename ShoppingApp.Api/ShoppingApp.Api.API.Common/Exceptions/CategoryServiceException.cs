using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Common.Exceptions
{
    public class CategoryServiceException : ServiceException
    {
        public CategoryServiceException(string message, Exception innerEx) : base(message, innerEx)
        {
        }

        public CategoryServiceException(string message) : base(message)
        {
        }

        public CategoryServiceException() : base()
        {
        }
    }
}