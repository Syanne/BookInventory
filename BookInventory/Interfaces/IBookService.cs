using BookInventory.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookInventory.Interfaces
{
    public interface IBookService
    {
        Task Add(string title, string author, string isbn, string publicationYear, int quantity, int categoryId);

        Task Update(int id, string title, string author, string isbn, string publicationYear, int quantity, int categoryId);

        Task Delete(int id);

        Task<BookVm> Get(int id);

        Task<IReadOnlyCollection<BookVm>> Get(int pageNumber, int pageSize, string search, bool byAsc);


        Task<IReadOnlyCollection<CategoryVm>> GetCategories();
    }
}
