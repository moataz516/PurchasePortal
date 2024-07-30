using AutoMapper;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.IService;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;
using PurchasePortal.Web.Models.Error;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Services
{
    public class PromotionCategoryService : IPromotionCategoryService
    {

        private readonly IMapper _mapper;
        private readonly IPromotionCategoryRepository _repository;
        private readonly ILogger<PromotionCategory> _logger;

        public PromotionCategoryService(ILogger<PromotionCategory> logger, IPromotionCategoryRepository repository , IMapper mapper )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IRepository.Result.IResult> AddAsync(CreatePromotionCategoryDto promotionCategory)
        {
            var result = new Result();
            try
            {
             result.IsSuccess = await _repository.AddAsync(_mapper.Map<PromotionCategory>(promotionCategory));

                if (result.IsSuccess)
                {
                    result.Message = "Promotion have been added successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured when adding promotion category, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.Message, 404, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;

        }

        public async Task<Result> DeleteAsync(string id)
        {
            var result = new Result();
            try
            {

                result.IsSuccess = await _repository.DeleteAsync(id);
                if (result.IsSuccess)
                {
                    result.Message = "Promotion have been Deleted successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured when deleting promotion, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.Message, 404, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<IDataResult<List<PromotionCategoryDto>>> GetAllAsync()
        {
            var dataResult = new DataResult<List<PromotionCategoryDto>>();
            try
            {
                var promotionCats = await _repository.GetAllAsync();
                if (promotionCats != null)
                {
                    dataResult.Result = _mapper.Map<List<PromotionCategoryDto>>(promotionCats);
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No promotion category found.", 404);
                }
                return dataResult;

            }
            catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
                return dataResult;
            }
        }

        public async Task<IDataResult<List<PromotionCategoryHomeDto>>> GetAllHomePageAsync()
        {
            var dataResult = new DataResult<List<PromotionCategoryHomeDto>>();
            try
            {
            var promotionCategories = await _repository.GetPromotionCategoryHome();
                if( promotionCategories != null )
                {
                dataResult.Result = _mapper.Map<List<PromotionCategoryHomeDto>>(promotionCategories);
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No promotion category found.", 404);
                }
                return dataResult;

            } catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
                return dataResult;
            }

        }

        public async Task<IRepository.Result.IResult> UpdateAsync(UpdatePromotionCategoryDto promotionCategory)
        {
            var result = new Result();
            try
            {
                var getPromotion = await _repository.GetByIdAsync(promotionCategory.Id);
                if (getPromotion == null)
                {
                    result.IsSuccess = false;
                    result.Error = new ErrorDetail("promotion not found", 404);
                    return result;
                }
                result.IsSuccess = await _repository.UpdateAsync(_mapper.Map(promotionCategory, getPromotion));
                if (result.IsSuccess)
                {
                    result.Message = "Promotion has been updated successfully!";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occurs", 404);
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

        public async Task<IDataResult<PromotionCategoryDto>> GetByIdAsync(string id)
        {
            var dataResult = new DataResult<PromotionCategoryDto>();

            try
            {
                var updatedPromotion = await _repository.GetByIdAsync(id);

                if (updatedPromotion == null)
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("The promotion with the specified ID does not exist.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<PromotionCategoryDto>(updatedPromotion);
                }

            }
            catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage} with Id '{id}', Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace},");
            }

            return dataResult;

        }

        public async Task<Result> TogglePromotionStatusAsync(string id)
        {
            var result = new Result();

            try
            {
                var getPromotion = await _repository.GetByIdAsync(id);

                if (getPromotion == null)
                {
                    result.IsSuccess = false;
                    result.Error = new ErrorDetail("The promotion with the specified ID does not exist.", 404);
                }
                getPromotion.IsActive = !getPromotion.IsActive;
                result.IsSuccess = await _repository.UpdateAsync(getPromotion);
                if (result.IsSuccess)
                {
                    result.Message = "Promotion updated his status successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Error = new ErrorDetail("An error occured while updating his status.", 404);
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} with Id '{id}', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }

            return result;
        }
    }
}
