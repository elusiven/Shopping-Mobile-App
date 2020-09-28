using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Application.Contracts
{
    public interface IStorageService
    {
        Task<T> GetObjectFromSecureStorageAsync<T>(string key);

        Task SetObjectInSecureStorageAsync(string key, object obj);

        void DeleteObjectInSecureStorage(string key);
    }
}