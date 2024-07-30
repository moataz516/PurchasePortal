using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models.DTOs.PromotionCategory
{
    public class UpdatePromotionCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public DateTime? UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
