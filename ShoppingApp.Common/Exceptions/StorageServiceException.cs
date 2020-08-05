using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Common.Exceptions
{
    public class StorageServiceException : ServiceException
    {
        public StorageServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}