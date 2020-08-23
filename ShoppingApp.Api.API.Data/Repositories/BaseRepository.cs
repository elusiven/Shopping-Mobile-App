using ShoppingApp.Api.API.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Api.API.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ShoppingDbContext _dbContext;
        internal DbSet<T> _dbSet;

        public BaseRepository(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T model)
        {
            var result = await _dbSet.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            _dbSet.Remove(await _dbSet.FindAsync(id));
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T model)
        {
            var result = _dbSet.Update(model);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}