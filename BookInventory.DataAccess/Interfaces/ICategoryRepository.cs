using BookInventory.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookInventory.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<Category>> Get();
    }
}
