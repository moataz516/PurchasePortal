using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Category;

namespace PurchasePortal.Web.IService
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepositpry
    {
        public CategoryRepository(ApplicationDbContext context): base(context) { }

        public async Task<List<Category>> GetProductsWithCategory()
        {
            var categories = await _dbSet.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Take(8).ToList()
            })
                .Take(5)
                .ToListAsync();


            return categories;
        }
    }
}
