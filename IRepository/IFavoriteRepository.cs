using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Favorite;

namespace PurchasePortal.Web.IRepository
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<FavoritItem>> GetFavoritesByUserAsync(string userId);

        Task<FavoritItem> IsFavoriteAsync(string userId, string productId);
        Task AddFavoriteAsync(FavoritItem favoritItem);
        Task ToggleFavoriteStatusAsync(string userId, string productId);
        Task<bool> IsFavoritStatusFalseAsync(string userId, string productId);
        Task<int> GetTotalFavoritesByProductAsync(string productId);
        Task<int> GetTotalFavoritesByUserAsync(string userId);
        Task UpdateFavoriteStatsAsync(FavoritItem favorit);

        //Task<FavoritItem> IsFavoriteAsync(FavoriteParamsDto favorite);
        //Task<bool> ChangeFavoriteStatusAsync(FavoriteParamsDto favorite);
        //Task<bool> AddFavoriteAsync(FavoritItem favorite);

        Task SaveChangesAsync();
    }
}
