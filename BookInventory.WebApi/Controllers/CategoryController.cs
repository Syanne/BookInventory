using BookInventory.DataAccess.Interfaces;
using BookInventory.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookInventory.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        readonly ICategoryRepository _repository;
        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<CategoryVm>> Get()
        {
            var result = await _repository.Get();

            return result
                .Select(x => new CategoryVm { Id = x.Id, CategoryName = x.CategoryName })
                .ToArray();
        }
    }
}
