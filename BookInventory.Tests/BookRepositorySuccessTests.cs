using BookInventory.DataAccess;
using BookInventory.DataAccess.Entities;
using BookInventory.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace BookInventory.Tests
{
    [TestClass]
    public class BookRepositorySuccessTests
    {
        public BookInventoryContext TestInitialize()
        {
            DbContextOptions<BookInventoryContext> options;
            var builder = new DbContextOptionsBuilder<BookInventoryContext>();
            builder.UseInMemoryDatabase("local");
            options = builder.Options;
            var ctx = new BookInventoryContext(options);

            return ctx;
        }

        [TestMethod]
        public async Task GetBook_Success()
        {
            var ctx = TestInitialize();
            ctx.Categories.Add(new Category { Id = 2, CategoryName = "Test", Description = "Some" });
            ctx.Books.Add(new Book
            {
                Id = 1,
                Title = "Test",
                Author = "Author 1",
                ISBN = "123123123",
                CategoryId = 2,
                PublicationYear = "2017",
                Quantity = 5
            });

            ctx.SaveChanges();

            //arrange
            var repository = new BookRepository(ctx);

            //act
            var book = await repository.Get(1);

            //assert
            Assert.IsNotNull(book);
            Assert.AreEqual(1, book.Id);
            Assert.AreEqual("Test", book.Title);
            Assert.AreEqual("Author 1", book.Author);
            Assert.AreEqual("123123123", book.ISBN);
            Assert.AreEqual("2017", book.PublicationYear);
            Assert.AreEqual(5, book.Quantity);
            Assert.AreEqual(2, book.CategoryId);
        }

        [TestMethod]
        public async Task DeleteBook_Success()
        {
            //arrange
            var ctx = TestInitialize();
            var repository = new BookRepository(ctx); 
            
            ctx.Categories.Add(new Category { Id = 3, CategoryName = "Test", Description = "Some"});

            ctx.Books.Add(new Book
            {
                Id = 12,
                Title = "Test2",
                Author = "Author",
                ISBN = "787857857578587855",
                CategoryId = 3,
                PublicationYear = "2015",
                Quantity = 5
            });

            ctx.SaveChanges();

            //act
            await repository.Delete(12);

            //assert
            Assert.IsTrue(!ctx.Books.Any(x=> x.Id == 12));
        }

        [TestMethod]
        public async Task UpdateBook_Success()
        {
            //arrange
            var ctx = TestInitialize();
            ctx.Categories.Add(new Category { Id = 1, CategoryName = "Test", Description = "Some" });
            ctx.Categories.Add(new Category { Id = 7, CategoryName = "Test 2", Description = "Some 33" });
            ctx.Books.Add(new Book
            {
                Id = 17,
                Title = "Test2",
                Author = "Author",
                ISBN = "787857857578587855",
                CategoryId = 1,
                PublicationYear = "2015",
                Quantity = 5
            });

            ctx.SaveChanges();
            var repository = new BookRepository(ctx);            

            //act
            await repository.Update(17, "new title", "new author", "33333", "1996", 4, 7);

            //assert
            var book = ctx.Books.FirstOrDefault(x => x.Id == 17);
            Assert.IsNotNull(book);
            Assert.AreEqual(17, book.Id);
            Assert.AreEqual("new title", book.Title);
            Assert.AreEqual("new author", book.Author);
            Assert.AreEqual("33333", book.ISBN);
            Assert.AreEqual("1996", book.PublicationYear);
            Assert.AreEqual(4, book.Quantity);
            Assert.AreEqual(7, book.CategoryId);
            Assert.AreEqual("Test 2", book.Category.CategoryName);
        }
    }
}
