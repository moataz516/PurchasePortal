using PurchasePortal.Web.Models.DTOs.Product;

namespace PurchasePortal.Web.Models.DTOs.PromotionCategory
{
    public class PromotionCategoryHomeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
