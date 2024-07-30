using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Models.DTOs.Shipping;

namespace PurchasePortal.Web.Repository
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(string userId, List<CartItemDto> cartItems, ShippingDto shippingAddress);

    }
}
