using BookInventory.DataAccess.Entities;
using BookInventory.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookInventoryContext _ctx;

        public CategoryRepository(BookInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IReadOnlyCollection<Category>> Get()
        {
            return await _ctx.Categories.ToListAsync();
        }
    }
}
