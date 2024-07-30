using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services;
using System.Security.Claims;

namespace PurchasePortal.Web.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        private string GetUserIdentifier()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
        }
        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("Favorits")]
        public async Task<IActionResult> Index()
        {
            var favorites = await _favoriteService.GetFavoritesByUserAsync(GetUserIdentifier());
            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> ToggleFavorite(string productId, string productName)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            await _favoriteService.ToggleFavoriteAsync(GetUserIdentifier(), productId);

            return RedirectToAction("ProductDetails", "Product" , new {slug=productName}) ;
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFavorite(string productId)
        {
            await _favoriteService.ToggleFavoriteAsync(GetUserIdentifier(), productId);
            return RedirectToAction("Index");
        }

        [HttpGet("GetTotalFavorite")]
        public async Task<IActionResult> GetTotalFavorite()
        {
            var totalFavorites = await _favoriteService.GetTotalFavoritesByUserAsync(GetUserIdentifier());
            return Json(new { count = totalFavorites });
        }
        [HttpPost]
        public async Task<IActionResult> ClearFavoritesAsync()
        {
            await _favoriteService.ClearFavoriteAsync(GetUserIdentifier());
            return RedirectToAction(nameof(Index));
        }


    }

}
