using PurchasePortal.Web.Models.DTOs.Product;

namespace PurchasePortal.Web.Models.DTOs.Category
{
    public class CategoryWithProductsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
