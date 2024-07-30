namespace PurchasePortal.Web.Models.DTOs.Product
{
    public class ProductIndexDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public decimal Discount { get; set; }
        public int QuantitySold { get; set; }
        public string CategoryName { get; set; }
    }
}
