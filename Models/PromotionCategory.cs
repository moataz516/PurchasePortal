using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    public class PromotionCategory : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductPromotion> ProductPromotions { get; set; } = new HashSet<ProductPromotion>();

    }
}
