using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;

namespace Juan_PB301EmilMusayev.Interfaces
{
    public interface ILayoutService
    {
        IDictionary<string, string> GetSettings();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<CartVM>> GetCartAsync();
    }
}