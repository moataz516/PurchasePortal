using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.Search;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.IRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetProductsByCategory(string categoryId, string sortBy, string filter);
        Task<Product>GetProductBySlug(string slug);
        Task<List<Product>> GetProductsBySearch(SearchDto search);
        Task<List<Product>> GetProductHomeFilter(string name);
        Task<List<Product>> GetProductsForAdmin();
    }
}
