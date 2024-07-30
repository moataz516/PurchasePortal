using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IPromotionCategoryService _promotionCategory;

        public CategoryController(ICategoryService categoryService, IPromotionCategoryService promotionCategoryService)
        {
            _categoryService = categoryService;
            _promotionCategory = promotionCategoryService;
        }


        [HttpGet("admin/category")]
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var promotionCats = await _promotionCategory.GetAllAsync();
            ViewData["PromotionCategories"] = promotionCats.Result;
            return View(categories.Result);
        }



        [HttpGet("EditCategoryModal/{id}")]
        public async Task<IActionResult> EditCategoryModal(string id)
        {
            var getCategory = await _categoryService.GetCategoryByIdAsync(id);
            var updateCategory = new UpdateCategoryDto()
            {
                Id = id,
                Name = getCategory.Result.Name,
                Description = getCategory.Result.Description,
            };
            return PartialView("CategoryModal/_EditCategoryModal", updateCategory);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("getC")]
        public async Task<List<CategoryDto>> getC()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return categories.Result;
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategory model)
        {
            try
            {
                var createdCategory = await _categoryService.AddCategoryAsync(model);
                if (createdCategory.IsSuccess)
                {
                    TempData["SuccessMessage"] = createdCategory.Message ?? "Category has been added successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurs";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCategoryDto model)
        {
            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(model);
                if (updatedCategory.IsSuccess)
                {
                    TempData["SuccessMessage"] = updatedCategory.Message ?? "Category has been updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurs";

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("category/delete/{id}")]
        public  async Task<IActionResult> DeleteCategoryModal(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return PartialView("CategoryModal/_DeleteCategoryModal", category.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
           var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
            if (deletedCategory.IsSuccess)
            {
                TempData["SuccessMessage"] = deletedCategory.Message ?? "Category has been deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurs";
            }
            return RedirectToAction("Index");
        }
    }
}
