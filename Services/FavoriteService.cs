using AutoMapper;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Favorite;
using PurchasePortal.Web.Repository;

namespace PurchasePortal.Web.Services
{
    public class FavoriteService : IFavoriteService
    {

        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        public async Task<bool> IsFavoriteAsync(string userId, string productId)
        {
            var isFavorite = await _favoriteRepository.IsFavoriteAsync(userId, productId);
            if(isFavorite == null)
            {
                return false;
            }
            return isFavorite.isActive;
        }

        public async Task ToggleFavoriteAsync(string userId, string productId)
        {
            var isFavorite = await _favoriteRepository.IsFavoriteAsync(userId, productId);

            if (isFavorite != null)
            {
               await _favoriteRepository.ToggleFavoriteStatusAsync(userId, productId);
            }
            else
            {
                var favoritItem = new FavoritItem
                {
                    UserId = userId,
                    ProductId = productId
                };
                await _favoriteRepository.AddFavoriteAsync(favoritItem);
            }
        }

        public async Task<IEnumerable<FavoritDto>> GetFavoritesByUserAsync(string userId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserAsync(userId);
            return _mapper.Map<IEnumerable<FavoritDto>>(favorites);
        }

        public async Task<int> GetTotalFavoritesByProductAsync(string productId)
        {
            return await _favoriteRepository.GetTotalFavoritesByProductAsync(productId);
        }

        public async Task<int> GetTotalFavoritesByUserAsync(string userId)
        {
            return await _favoriteRepository.GetTotalFavoritesByUserAsync(userId);
        }

        public async Task ClearFavoriteAsync(string userId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserAsync(userId);
            if (favorites != null)
            {
                foreach (var favorite in favorites)
                {
                    favorite.isActive = false;
                    await _favoriteRepository.UpdateFavoriteStatsAsync(favorite);
                }
            }
        }
    }
}
