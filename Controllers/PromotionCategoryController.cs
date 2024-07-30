using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Controllers
{
    public class PromotionCategoryController : Controller
    {
        private readonly IPromotionCategoryService _promotionCategoryService;

        public PromotionCategoryController(IPromotionCategoryService promotionCategoryService)
        {
            _promotionCategoryService = promotionCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _promotionCategoryService.GetAllAsync();
            return View(result);
        }

        [HttpGet("promotion/edit/{id}")]
        public async Task<IActionResult> EditPromotionModal(string id) {
            var result = await _promotionCategoryService.GetByIdAsync(id);
            var updatePC = new UpdatePromotionCategoryDto
            {
                Id = id,
                Name = result.Result.Name,
                isActive = result.Result.IsActive,
            };
            return PartialView("PromotionCategoryModal/_UpdatePromotionCategory", updatePC);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (UpdatePromotionCategoryDto model)
        {
            var result = await _promotionCategoryService.UpdateAsync(model);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message ?? "Promotion has been updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occured";
            }
            return RedirectToAction("Index", "Category");
        }


        [HttpPost]
        public async Task<IActionResult> TogglePromotionStatus(string id)
        {
            var result = await _promotionCategoryService.TogglePromotionStatusAsync(id);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurs";
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePromotionCategoryDto modal)
        {
            var result = await _promotionCategoryService.AddAsync(modal);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message ?? "Category has been deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurs";
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet("promotion/delete/{id}")]
        public async Task<IActionResult> DeletePromotionModal(string id)
        {
            var deletePromotion = await _promotionCategoryService.GetByIdAsync(id);
            var result = new DeletePromotionDto { Id = id, Name=deletePromotion.Result.Name };
            return PartialView("PromotionCategoryModal/_DeletePromotionModal", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedPromotion = await _promotionCategoryService.DeleteAsync(id);
            if (deletedPromotion.IsSuccess)
            {
                TempData["SuccessMessage"] = deletedPromotion.Message ?? "Promotion has been deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occured";
            }
            return RedirectToAction("Index", "Category");
        }
    }
}
