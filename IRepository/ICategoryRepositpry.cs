using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Category;

namespace PurchasePortal.Web.IRepository
{
    public interface ICategoryRepositpry : IBaseRepository<Category>
    {
        Task<List<Category>> GetProductsWithCategory();

    }
}
