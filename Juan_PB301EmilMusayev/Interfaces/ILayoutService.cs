using Juan_PB301EmilMusayev.Models;

namespace Juan_PB301EmilMusayev.Interfaces
{
    public interface ILayoutService
    {
        IDictionary<string, string> GetSettings();
        Task<IEnumerable<Product>> GetProducts();
    }
}