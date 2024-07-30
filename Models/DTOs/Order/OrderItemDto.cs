using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models.DTOs.Order
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
    }
}
