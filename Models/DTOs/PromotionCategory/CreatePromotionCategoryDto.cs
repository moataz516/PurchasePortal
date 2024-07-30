namespace PurchasePortal.Web.Models.DTOs.PromotionCategory
{
    public class CreatePromotionCategoryDto
    {
        public string Name { get; set; }
        public bool isActive { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
