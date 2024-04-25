using BookInventory.Interfaces;
using BookInventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace BookInventory.Services
{
    internal class BookService : IBookService
    {
        private readonly IBookInventoryClient _client;
        public BookService(IBookInventoryClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<BookVm>> Get(int pageNumber, int pageSize, string search, bool byAsc)
        {
            var query = new Dictionary<string, string>
            {
                { nameof(pageNumber), pageNumber.ToString() },
                { nameof(pageSize), pageSize.ToString() },
                { nameof(search), search },
                { nameof(byAsc), byAsc.ToString() }
            };

            var books = await _client.Get<List<BookVm>>("api/books", query);

            return books;
        }

        public async Task Delete(int id)
        {
            await _client.Delete("api/books/"+ id);
        }

        public async Task<BookVm> Get(int id)
        {
            var books = await _client.Get<BookVm> ("api/books/" + id);

            return books;
        }
        public async Task Add(string title, string author, string isbn, string publicationYear, int quantity, int categoryId)
        {
            var obj = new
            {
                title,
                author,
                isbn,
                publicationYear,
                quantity,
                categoryId
            };

            var query = new Dictionary<string, string>
            {
                { nameof(title), title },
                { nameof(author), author },
                { nameof(isbn), isbn },
                { nameof(publicationYear), publicationYear },
                { nameof(quantity), quantity.ToString() },
                { nameof(categoryId), categoryId.ToString() }
            };

            await _client.Post("api/books", query);
        }

        public async Task Update(int id, string title, string author, string isbn, string publicationYear, int quantity, int categoryId)
        {
            var query = new Dictionary<string, string>
            {
                { nameof(title), title },
                { nameof(author), author },
                { nameof(isbn), isbn },
                { nameof(publicationYear), publicationYear },
                { nameof(quantity), quantity.ToString() },
                { nameof(categoryId), categoryId.ToString() }
            };

            await _client.Put("api/books/" + id, query);
        }

        public async Task<IReadOnlyCollection<CategoryVm>> GetCategories()
        {
            var categories = await _client.Get<List<CategoryVm>>("api/category");

            return categories;
        }
    }
}
