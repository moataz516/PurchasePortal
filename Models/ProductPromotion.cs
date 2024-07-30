using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    public class ProductPromotion : BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; } 

        public string PromotionCategoryId { get; set; }
        public PromotionCategory PromotionCategory { get; set; }
    }
}
