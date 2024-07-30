using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;

namespace PurchasePortal.Web.Models.DTOs
{
    public class HomeDto
    {

        public ICollection<ProductDto> Products { get; set; } 
        public ICollection<CategoryWithProductsDto> CategoriesWithProducts { get; set; }
        public ICollection<PromotionCategoryHomeDto> PromotionCategories { get; set; }
        //public CreateProduct CreateProduct { get; set; }
        //public UpdateProductDto UpdateProduct { get; set; }
    }
}
