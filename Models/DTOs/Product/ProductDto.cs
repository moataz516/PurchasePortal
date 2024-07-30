using PurchasePortal.Web.Models.DTOs.Category;

namespace PurchasePortal.Web.Models.DTOs.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public string Image {  get; set; }  
        
    }
}
