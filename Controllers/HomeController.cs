using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;
using PurchasePortal.Web.Repository;
using System.Diagnostics;
using System.Security.Claims;

namespace PurchasePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _cat;
        private readonly IProductService _pro;
        private readonly IPromotionCategoryService _promotionCategory;
        private readonly IFavoriteService _favoriteService;

        public HomeController(ILogger<HomeController> logger, ICategoryService cat, IProductService pro, IPromotionCategoryService promotionCategory, IFavoriteService favoriteService)
        {
            _logger = logger;
            _cat = cat;
            _pro = pro;
            _promotionCategory = promotionCategory;
            _favoriteService = favoriteService;
        }



        public async Task<ActionResult> Index()
        {

            //var categories = await _cat.GetAllCategoriesAsync();
            var products = await _pro.GetAllProductsAsync();
            var categoriesWithProds = await _cat.GetProductsWithCategoryAsync();
            var promotionCategories = await _promotionCategory.GetAllHomePageAsync();

            var viewData = new HomeDto
            {
                //Categories = categories.IsSuccess ? categories.Result : new List<CategoryDto>(),
                Products = products.Result ?? new List<ProductDto>(),
                CategoriesWithProducts = categoriesWithProds.Result ?? new List<CategoryWithProductsDto>(),
                PromotionCategories = promotionCategories.Result ?? new List<PromotionCategoryHomeDto>(),
            };

            return View(viewData);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
