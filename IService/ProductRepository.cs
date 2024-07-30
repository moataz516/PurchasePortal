using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.Search;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.IService
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository( ApplicationDbContext _context) : base(_context)
        {
        }

        public async Task<Product> GetProductBySlug(string slug)
        {
            var product = await _dbSet.FirstOrDefaultAsync(p => p.Slug == slug);
            return product;
        }

        public async Task<List<Product>> GetProductHomeFilter(string name)
        {
            var result = await _dbSet.Where(p => p.Category.Name == name).Take(6).ToListAsync();
            return result;
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryId, string sortBy, string filter)
        {

            var query = _dbSet.AsQueryable();
            if(!string.IsNullOrEmpty(categoryId) )
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }
            if(!string.IsNullOrEmpty(filter) )
            {
                query = query.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
            }
            switch(sortBy)
            {
                case "name":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "stock":
                    query = query.OrderBy(p => p.StockQuantity);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }
            return await query.ToListAsync();

            //return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Product>> GetProductsBySearch(SearchDto search)
        {


            var query = _dbSet.AsQueryable();
            if (!string.IsNullOrEmpty(search.catId))
            {
                query = query.Where(p => p.CategoryId == search.catId);
            }
            if (!string.IsNullOrEmpty(search.filter))
            {
                query = query.Where(p => p.Name.Contains(search.filter));
            }            
            if (search.desc)
            {
                query = query.Where(p => p.Description.Contains(search.filter));
            }
            switch (search.sortBy)
            {
                case "name":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "stock":
                    query = query.OrderBy(p => p.StockQuantity);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }
            return await query.ToListAsync();




        }

        public async Task<List<Product>> GetProductsForAdmin()
        {
            return await _dbSet.Include(p => p.Category).ToListAsync();
        }
    }
}
