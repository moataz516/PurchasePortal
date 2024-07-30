using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IRepository
{
    public interface ICartRepository
    {
        Task AddToCartAsync(CartItem cartItem);
        Task MarkCartItemAsInactiveAsync(string userId, string productId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task ClearCartAsync(string userId);
        Task<IEnumerable<CartItem>> GetActiveCartItemsByUserAsync(string userId);
        Task<CartItem> GetActiveCartItemAsync(string userId, string productId);
        Task<int> GetTotalCartItemByUserAsync(string userId);
    }
}
