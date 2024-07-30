using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models.DTOs.PromotionCategory
{
    public class PromotionCategoryDto : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
