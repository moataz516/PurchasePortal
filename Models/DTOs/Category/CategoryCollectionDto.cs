using PurchasePortal.Web.Models.DTOs.PromotionCategory;

namespace PurchasePortal.Web.Models.DTOs.Category
{
    public class CategoryCollectionDto
    {
        public List<CategoryDto> Categories { get; set; }
        public List<PromotionCategoryHomeDto> PromotionCategories { get; set; }
    }
}
