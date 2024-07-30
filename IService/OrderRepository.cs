using PurchasePortal.Web.Data;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IService
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
