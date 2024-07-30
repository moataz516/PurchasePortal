using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Cart;

namespace PurchasePortal.Web.Repository
{
    public interface ICartService
    {
        Task AddToCartAsync(AddToCartDto request);
        Task MarkCartItemAsInactiveAsync(MarkCartItemAsInactiveDto request);
        Task UpdateCartItemQuantityAsync(UpdateCartItemQuantityDto request);
        Task ClearCartAsync(string userId);
        Task<IEnumerable<CartItemDto>> GetActiveCartItemsByUserAsync(string userId);
        Task<int> GetTotalCartItemsByUserAsync(string userId);

    }
}
