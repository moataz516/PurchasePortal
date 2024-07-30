using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Category;

namespace PurchasePortal.Web.Repository
{
    public interface ICategoryService
    {
        Task <IDataResult<List<CategoryDto>>> GetAllCategoriesAsync();
        Task <IDataResult<CategoryDto>> GetCategoryByIdAsync(string id);
        Task<IRepository.Result.IResult> AddCategoryAsync(CreateCategory category);
        Task<IRepository.Result.IResult> UpdateCategoryAsync(UpdateCategoryDto category);
        Task<IRepository.Result.IResult> DeleteCategoryAsync(string id);
        Task<IDataResult<List<CategoryWithProductsDto>>> GetProductsWithCategoryAsync();


    }
}
