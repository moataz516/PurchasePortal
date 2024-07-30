using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Favorite;

namespace PurchasePortal.Web.Repository
{
    public interface IFavoriteService
    {
        Task<bool> IsFavoriteAsync(string userId, string productId);
        Task ToggleFavoriteAsync(string userId, string productId);
        Task<IEnumerable<FavoritDto>> GetFavoritesByUserAsync(string userId);
        Task ClearFavoriteAsync(string userId);
        Task<int> GetTotalFavoritesByProductAsync(string productId);
        Task<int> GetTotalFavoritesByUserAsync(string userId);
    }
}
