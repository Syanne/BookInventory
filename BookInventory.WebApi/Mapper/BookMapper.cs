using BookInventory.DataAccess.Entities;
using BookInventory.Models.ViewModels;

namespace BookInventory.Api.Mapper
{
    //a quick solution that could be replaced with AutoMapper
    public static class BookMapper
    {
        public static BookVm Map(this Book book)
        {
            return new BookVm
            {
                Id = book.Id,
                ISBN = book.ISBN,
                CategoryName = book.Category?.CategoryName,
                Title = book.Title,
                Author = book.Author,
                Quantity = book.Quantity,
                PublicationYear = book.PublicationYear,
                CategoryId = book.CategoryId
            };
        }
    }
}