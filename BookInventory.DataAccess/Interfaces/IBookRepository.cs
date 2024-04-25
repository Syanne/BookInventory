using BookInventory.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookInventory.DataAccess.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> Add(string title, string author, string isbn, string publicationYear, int quantity, int categoryId);

        Task Update(int id, string title, string author, string isbn, string publicationYear, int quantity, int categoryId);

        Task Delete(int id);

        Task<Book> Get(int id);

        IReadOnlyCollection<Book> Get(int pageNumber, int pageSize, string search, bool byAsc);
    }
}
