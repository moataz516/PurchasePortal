using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Repository
{
    public interface IPromotionCategoryService
    {
        Task<IDataResult<List<PromotionCategoryDto>>> GetAllAsync();
        Task<IDataResult<List<PromotionCategoryHomeDto>>> GetAllHomePageAsync();
        Task<IDataResult<PromotionCategoryDto>> GetByIdAsync(string id);
        Task<IRepository.Result.IResult> AddAsync(CreatePromotionCategoryDto promotionCategory);
        Task<IRepository.Result.IResult> UpdateAsync(UpdatePromotionCategoryDto promotionCategory);
        Task<Result> DeleteAsync(string id);
        Task<Result> TogglePromotionStatusAsync(string id);

    }
}
