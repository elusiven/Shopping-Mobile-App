using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Api.API.Data.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(object id);

        Task<ICollection<T>> GetAllAsync();

        Task<T> CreateAsync(T model);

        Task<T> UpdateAsync(T model);

        Task<bool> DeleteAsync(object id);
    }
}