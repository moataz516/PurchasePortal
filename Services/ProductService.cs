using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.Search;
using PurchasePortal.Web.Models.Error;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;
using System.Security.Claims;

namespace PurchasePortal.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IFileService _fileService;
        private readonly IProductPromotionRepository _productpromotion;
        private readonly IFavoriteRepository _favoriteRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger, IFileService fileService, IProductPromotionRepository productpromotion, IFavoriteRepository favoriteRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _fileService = fileService;
            _productpromotion = productpromotion;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IDataResult<List<ProductDto>>> GetAllProductsAsync()
        {
            var  dataResult = new DataResult<List<ProductDto>>();
            try 
            {
                var products = await _productRepository.GetAllAsync();
                if (products == null || !products.Any())
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No product found.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<List<ProductDto>>(products);
                }
            }
            catch (Exception ex)
            {

                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;
        }

        public async Task<IDataResult<ProductDto>> GetProductByIdAsync(string id)
        {
            var dataResult = new DataResult<ProductDto>();
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("The product with the specified ID does not exist.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<ProductDto>(product);
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

        public async Task<Result> AddProductAsync(CreateProduct product)
        {
            var result = new Result();
            try
            {
                if (product.ImageFile != null || product.ImageFile.Length > 0)
                {
                    product.Image = await _fileService.UploadFileAsync(product.ImageFile, "product/image");
                }
                var pid = _mapper.Map<Product>(product);
                result.IsSuccess = await _productRepository.AddAsync(pid);

                if (result.IsSuccess)
                {
                    result.Message = "Product has been added successfully";
                    var promotionCats = new List<ProductPromotion>();
                    foreach(var pcId in product.SelectedPromotions)
                    {
                        promotionCats.Add(new ProductPromotion
                        {
                            ProductId = pid.Id,
                            PromotionCategoryId = pcId
                        });
                    }
                    await _productpromotion.AddRangeAsync(promotionCats);
                    
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured when adding product, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.InnerException.Message, 404, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<Result> UpdateProductAsync(UpdateProductDto product)
        {
            var result = new Result();
            try
            {
                var updateProduct = await _productRepository.GetByIdAsync(product.Id);
                if (updateProduct == null)
                {
                    result.IsSuccess = false;
                    result.Error = new ErrorDetail("Product not found", 404);
                    return result;
                }
                
                result.IsSuccess = await _productRepository.UpdateAsync(_mapper.Map(product, updateProduct));

                if (result.IsSuccess)
                {
                    result.Message = "Product have been updated successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured while updating product, Please try again!", 404);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<IRepository.Result.IResult> DeleteProductAsync(string id)
        {
            var result = new Result();
            try
            {
                var productImage = await _productRepository.GetByIdAsync(id);
                if(productImage != null)
                    _fileService.DeleteFile(productImage.Image);

                result.IsSuccess = await _productRepository.DeleteAsync(id);
                if (result.IsSuccess)
                {

                    result.Message = "Product have been deleted successfully";
                }
                else
                {
                    result.Error = new ErrorDetail("An error occured while deleting product, Please try again!", 404);
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
            }
            return result;
        }

        public async Task<DataResult<List<ProductDto>>> GetProductsByCategoryAsync(string categoryId, string sortBy, string filter)
        {
            var  dataResult = new DataResult<List<ProductDto>>();
            try 
            {
                var products = await _productRepository.GetProductsByCategory(categoryId, sortBy, filter);
                if (products == null || !products.Any())
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("There is no product found", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<List<ProductDto>>(products);
                }
            }
            catch (Exception ex)
            {

                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;        
        }

        public async Task<DataResult<List<ProductDto>>> GetProductsBySearchAsync(SearchDto search)
        {
            var result = new DataResult<List<ProductDto>>();
            try
            {
                var products = await _productRepository.GetProductsBySearch(search);
                if(products == null || !products.Any())
                {
                    result.IsSuccess = false;
                    result.Error = new ErrorDetail("No product found.", 404);
                }
                else
                {
                result.Result = _mapper.Map<List<ProductDto>>(products);
                }
            } 
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {result.Error.ErrorMessage}, Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace}");
            }
            return result;
        }

        public async Task<IDataResult<ProductDetailsDto>> GetProductBySlugAsync(string slug, string userId)
        {
            var dataResult = new DataResult<ProductDetailsDto>();
            try
            {
                var product = await _productRepository.GetProductBySlug(slug);

                if (product == null)
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("The product with the specified ID does not exist.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<ProductDetailsDto>(product);
                    var a = await _favoriteRepository.IsFavoriteAsync(userId, product.Id);
                    dataResult.Result.isFavorite = a == null ? false : a.isActive;
                }
            }
            catch (Exception ex)
            {
                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage} with this name '{slug}', Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace},");
            }

            return dataResult;
        }

        public async Task<IDataResult<List<ProductDto>>> GetProductsByFilter(string name)
        {
            var dataResult = new DataResult<List<ProductDto>>();
            try
            {
                var products = await _productRepository.GetProductHomeFilter(name);
                if (products == null || !products.Any())
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No product found.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<List<ProductDto>>(products);
                }
            }
            catch (Exception ex)
            {

                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;
        }

        public async Task<IDataResult<List<ProductIndexDto>>> GetProductsForAdminAsync()
        {
            var dataResult = new DataResult<List<ProductIndexDto>>();
            try
            {
                var products = await _productRepository.GetProductsForAdmin();
                if (products == null || !products.Any())
                {
                    dataResult.IsSuccess = false;
                    dataResult.Error = new ErrorDetail("No product found.", 404);
                }
                else
                {
                    dataResult.Result = _mapper.Map<List<ProductIndexDto>>(products);
                }
            }
            catch (Exception ex)
            {

                dataResult.IsSuccess = false;
                dataResult.Error = new ErrorDetail(ex.InnerException.Message, 500, ex.StackTrace);
                _logger.LogError($"Error details: {dataResult.Error.ErrorMessage}, Code: {dataResult.Error.ErrorCode}, StackTrace: {dataResult.Error.StackTrace}");
            }
            return dataResult;
        }
    }
}
