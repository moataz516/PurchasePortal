namespace PurchasePortal.Web.Models.DTOs.Cart
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
