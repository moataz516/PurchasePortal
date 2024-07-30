using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.Search;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Repository
{
    public interface IProductService
    {
        Task<IDataResult<List<ProductDto>>> GetAllProductsAsync();
        Task<IDataResult<List<ProductIndexDto>>> GetProductsForAdminAsync();
        Task<IDataResult<ProductDto>> GetProductByIdAsync(string id);
        Task<Result> AddProductAsync(CreateProduct product);
        Task<Result> UpdateProductAsync(UpdateProductDto product);
        Task<IRepository.Result.IResult> DeleteProductAsync(string id);
        Task<DataResult<List<ProductDto>>> GetProductsByCategoryAsync(string categoryId, string sortBy, string filter);
        Task<DataResult<List<ProductDto>>> GetProductsBySearchAsync(SearchDto search);
        Task<IDataResult<ProductDetailsDto>> GetProductBySlugAsync(string slug, string userId);
        Task<IDataResult<List<ProductDto>>> GetProductsByFilter(string name);

    }
}
