using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.IRepository
{
    public interface IOrderRepository
    {
        Task<string> CreateOrderAsync(Order order);
    }
}
