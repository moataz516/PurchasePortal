using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Repository;

namespace PurchasePortal.Web.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories.Result);
        }

    }
}

