using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IRepository
{
    public interface IPromotionCategoryRepository : IBaseRepository<PromotionCategory>
    {
        Task<List<PromotionCategory>> GetPromotionCategoryHome();
    }
}
