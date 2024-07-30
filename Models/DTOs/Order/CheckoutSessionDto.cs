using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Models.DTOs.Shipping;

namespace PurchasePortal.Web.Models.DTOs.Order
{
    public class CheckoutSessionDto
    {
        public List<CartItemDto> CartItems { get; set; }
        public ShippingDto ShippingAddress { get; set; }
        //public PaymentDto PaymentDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
