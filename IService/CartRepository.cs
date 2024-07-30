using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IService
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddToCartAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task MarkCartItemAsInactiveAsync(string userId, string productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId && c.IsActive);
            if (cartItem != null)
            {
                cartItem.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _context.CartItems.Where(c => c.UserId == userId && c.IsActive).ToListAsync();
            foreach (var cartItem in cartItems)
            {
                cartItem.IsActive = false;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetActiveCartItemsByUserAsync(string userId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId && c.IsActive)
                .ToListAsync();
        }

        public async Task<CartItem> GetActiveCartItemAsync(string userId, string productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId  );
        }

        public Task<int> GetTotalCartItemByUserAsync(string userId)
        {
            return _context.CartItems.CountAsync(c => c.UserId == userId && c.IsActive == true);
        }
    }
}
