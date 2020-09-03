using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Api.API.Data.Contracts;
using ShoppingApp.Api.API.Data.Entities;

namespace ShoppingApp.Api.API.Data.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ShoppingDbContext dbContext) : base(dbContext)
        {
        }
    }
}