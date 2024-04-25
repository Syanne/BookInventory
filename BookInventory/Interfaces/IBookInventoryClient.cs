using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookInventory.Interfaces
{
    public interface IBookInventoryClient
    {
        Task<T> Get<T>(string action, Dictionary<string, string> query = null)
            where T : class;
        Task Put(string action, Dictionary<string, string> request);
        Task Post(string action, Dictionary<string, string> request);
        Task Delete(string action);

    }
}
