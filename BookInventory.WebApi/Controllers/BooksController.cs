using BookInventory.Api.Mapper;
using BookInventory.Api.Models;
using BookInventory.DataAccess.Interfaces;
using BookInventory.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookInventory.WebApi.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public BooksController(IBookRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<BookVm>> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string search = "",
            bool byAsc = false)
        {
            var books = _repository.Get(pageNumber, pageSize, search, byAsc);

            var categories = await _categoryRepository.Get();

            foreach (var b in books)
            {
                b.Category = categories.First(x => x.Id == b.CategoryId);
            }

            return books
                .Select(x => x.Map())
                .ToList();
        }

        public async Task<BookVm> Get(int id)
        {
            var entity = await _repository.Get(id);

            return entity.Map();
        }

        // POST api/values
        public async Task Post([FromBody] BookManagementModel model)
        {
            await _repository.Add(model.Title,
                model.Author,
                model.ISBN,
                model.PublicationYear,
                model.Quantity,
                model.CategoryId);
        }

        // PUT api/values/5
        public async Task Put(int id, [FromBody] BookManagementModel model)
        {
            await _repository.Update(id,
                model.Title,
                model.Author,
                model.ISBN,
                model.PublicationYear,
                model.Quantity,
                model.CategoryId);
        }

        // DELETE api/values/5
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
