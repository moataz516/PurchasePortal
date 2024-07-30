using AutoMapper;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Repository;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Services.ResultService;
using PurchasePortal.Web.Models.Error;

namespace PurchasePortal.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepositpry _repository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IPromotionCategoryService _promotionCategoryService;

        public CategoryService(IMapper mapper, ICategoryRepositpry repository, ILogger<CategoryService> logger, IPromotionCategoryService promotionCategoryService)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _promotionCategoryService = promotionCategoryService;
        }

        #region Read Methods
        public async Task<IDataResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var dataResult = new DataResult<List<CategoryDto>>();
            try 
            { 
                var categories = await _repository.GetAllAsync();
                if(categories == null || categories.Count == 0) 
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No categories found in the collection", 404);
                }
                else
                {
                    dataResult.Message = "Category found";
                    dataResult.Result = _mapper.Map<List<CategoryDto>>(categories);
                }
            }
            catch(Exception ex) 
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;
        }

        public async Task<IDataResult<CategoryDto>> GetCategoryByIdAsync(string id)
        {
            var dataResult = new DataResult<CategoryDto>();
            try
            {
                var category = await _repository.GetByIdAsync(id);

                if (category == null)
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("Category not found", 404);
                }
                else
                {
                    dataResult.Message = "Category found";
                    dataResult.Result = _mapper.Map<CategoryDto>(category);
                }
            }
            catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage} with Id '{id}', Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace},");
            }

            return dataResult;
        }
        #endregion


        public async Task<IRepository.Result.IResult> AddCategoryAsync(CreateCategory model)
        {
            var result = new Result();
            try
            {
                result.IsSuccess = await _repository.AddAsync(_mapper.Map<Category>(model));

                if(result.IsSuccess)
                {
                    result.Message = "Category have been added successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured when adding category, Please try again!", 404);
                }
            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.Message, 404 , ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<IRepository.Result.IResult> UpdateCategoryAsync(UpdateCategoryDto model)
        {
           
            var result = new Result();
            try
            {
                var updateCategory = await _repository.GetByIdAsync(model.Id);
                if (updateCategory == null)
                {
                    result.IsSuccess= false;
                    result.Error = new ErrorDetail("Category not found",404);
                    return result;
                }

                _mapper.Map(model, updateCategory);
                result.IsSuccess = await _repository.UpdateAsync(updateCategory);

                if (result.IsSuccess)
                {
                    result.Message = "Category have been updated successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured while updating category, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;

        }

        public async Task<IRepository.Result.IResult> DeleteCategoryAsync(string id)
        {
            var result = new Result();
            try
            {
                result.IsSuccess = await _repository.DeleteAsync(id);
                if (result.IsSuccess)
                {
                    result.Message = "Category have been deleted successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured while deleting category, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<IDataResult<List<CategoryWithProductsDto>>> GetProductsWithCategoryAsync()
        {
            var dataResult = new DataResult<List<CategoryWithProductsDto>>();
            try
            {
                var categories = await _repository.GetProductsWithCategory();
                if (categories == null || categories.Count == 0)
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No categories found in the collection", 404);
                }
                else
                {
                    dataResult.Message = "Category found";
                    dataResult.Result = _mapper.Map<List<CategoryWithProductsDto>>(categories);
                }
            }
            catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;
        }
    }
}
