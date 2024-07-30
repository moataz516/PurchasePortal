namespace PurchasePortal.Web.Models.DTOs.Favorite
{
    public class FavoritDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
    }
}
