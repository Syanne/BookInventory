using BookInventory.DataAccess.Entities;
using BookInventory.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookInventoryContext _ctx;

        public BookRepository(BookInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Book> Add(string title,
            string author,
            string isbn,
            string publicationYear,
            int quantity,
            int categoryId)
        {
            if (await _ctx.Books.AnyAsync(x => x.ISBN.Equals(isbn)))
            {
                throw new ArgumentException($"A book with ISBN {isbn} already exists");
            }

            var book = new Book()
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                PublicationYear = publicationYear,
                Quantity = quantity,
                CategoryId = categoryId
            };

            _ctx.Books.Add(book);
            await _ctx.SaveChangesAsync();

            return book;
        }

        
        public async Task Delete(int id)
        {
            var book = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                throw new ArgumentException($"A book with Id {id} does not exist");
            }

            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task Update(int id,
            string title,
            string author,
            string isbn,
            string publicationYear,
            int quantity,
            int categoryId)
        {
            var book = await GetBookOrThrow(id);

            book.Title = title;
            book.Author = author;
            book.ISBN = isbn;
            book.PublicationYear = publicationYear;
            book.Quantity = quantity;
            book.CategoryId = categoryId;

            await _ctx.SaveChangesAsync();
        }

        public async Task<Book> Get(int id)
        {
            var book = await GetBookOrThrow(id);

            return book;
        }

        public IReadOnlyCollection<Book> Get(
            int pageNumber = 1,
            int pageSize = 10,
            string search = null,
            bool byAsc = false)
        {
            //var searchParam = new SqlParameter("@search", search);
            //var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            //var pageSizeParam = new SqlParameter("@pageSize", pageSize);
            //var ascParam = new SqlParameter("@asc", byAsc);

            //full text does not support whitespaces - let's replace them
            if(!string.IsNullOrEmpty(search)){
                var rearrangedSearch = search.Split(' ').Where(x=> !string.IsNullOrWhiteSpace(x));

                search = string.Join("%", rearrangedSearch);
            }

            var books = _ctx
                .Set<Book>()
                .FromSqlRaw($"EXECUTE SearchABook '{search}', {pageNumber}, {pageSize}, {byAsc};")
                .ToArray();

            return books.ToArray();
        }

        private async Task<Book> GetBookOrThrow(int id)
        {
            var b = _ctx.Books.FirstOrDefault();
            if (!_ctx.Books.Any(x => x.Id == id))
            {
                throw new ArgumentException($"A book with Id {id} does not exist");
            }

            var book = await _ctx.Books
                .Include(x => x.Category)
                .FirstAsync(x => x.Id == id);

            return book;
        }
    }
}
