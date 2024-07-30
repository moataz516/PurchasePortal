using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IService
{
    public class PromotionCategoryRepository : BaseRepository<PromotionCategory>, IPromotionCategoryRepository
    {
        public PromotionCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<PromotionCategory>> GetPromotionCategoryHome()
        {
            var result = await _dbSet
                .Where(pc => pc.IsActive == true)
                .Include(pp => pp.ProductPromotions)
                .ThenInclude(p => p.Product)
                .ToListAsync();

            return result;
        }
    }
}
