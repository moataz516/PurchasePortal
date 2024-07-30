using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IService
{
    public class ProductPromotionRepository : BaseRepository<ProductPromotion>, IProductPromotionRepository
    {
        public ProductPromotionRepository(ApplicationDbContext context) : base(context) { }

    }

}
