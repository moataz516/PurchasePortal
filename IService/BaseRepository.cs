using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Update;
using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;
using System.Linq.Expressions;

namespace PurchasePortal.Web.IService
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected  DbSet<T> _dbSet;
        protected  ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var added = await _context.SaveChangesAsync();
            return added > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if(entity == null) return false;
            _dbSet.Remove(entity);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

    }
}
