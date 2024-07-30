namespace PurchasePortal.Web.Models.DTOs.Cart
{
    public class CartItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
    }
}
