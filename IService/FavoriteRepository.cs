using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IService
{
    public class FavoriteRepository : IFavoriteRepository
    {

        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddFavoriteAsync(FavoritItem favoritItem)
        {
            await _context.FavoritItems.AddAsync(favoritItem);
            await SaveChangesAsync();
        }


        public async Task<IEnumerable<FavoritItem>> GetFavoritesByUserAsync(string userId)
        {
            return await _context.FavoritItems
                .Where(f => f.UserId == userId && f.isActive == true).Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<FavoritItem> IsFavoriteAsync(string userId, string productId)
        {
            return await _context.FavoritItems
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
        }


        public async Task ToggleFavoriteStatusAsync(string userId, string productId)
        {
            var favoritItem = await IsFavoriteAsync(userId, productId);

            if (favoritItem != null)
            {
                favoritItem.isActive = !favoritItem.isActive;
                _context.FavoritItems.Update(favoritItem);
                await SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalFavoritesByUserAsync(string userId)
        {
            return await _context.FavoritItems
                .CountAsync(f => f.UserId == userId && f.isActive == true);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalFavoritesByProductAsync(string productId)
        {
            return await _context.FavoritItems
                .CountAsync(f => f.ProductId == productId);
        }

        public async Task<bool> IsFavoritStatusFalseAsync(string userId, string productId)
        {
            return await _context.FavoritItems.AnyAsync(f => f.UserId == userId && f.ProductId == productId && f.isActive == false);
        }

        public async Task UpdateFavoriteStatsAsync(FavoritItem favorit)
        {
            _context.Update(favorit);
            await SaveChangesAsync();
        }
    }
}
