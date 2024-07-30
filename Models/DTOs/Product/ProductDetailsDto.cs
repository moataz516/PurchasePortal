namespace PurchasePortal.Web.Models.DTOs.Product
{
    public class ProductDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public bool isFavorite { get; set; }

    }
}
