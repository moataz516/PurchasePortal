namespace PurchasePortal.Web.Models.DTOs.Cart
{
    public class UpdateCartItemQuantityDto
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
