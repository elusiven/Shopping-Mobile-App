using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingApp.Application.Contracts;
using Xamarin.Essentials;

namespace ShoppingApp.Application.Services
{
    public class StorageService : IStorageService
    {
        public async Task<T> GetObjectFromSecureStorageAsync<T>(string key)
        {
            var obj = await SecureStorage.GetAsync(key);
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public async Task SetObjectInSecureStorageAsync(string key, object obj)
        {
            var jsonObjString = JsonConvert.SerializeObject(obj);
            await SecureStorage.SetAsync(key, jsonObjString);
        }
    }
}