using PurchasePortal.Web.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models.DTOs.Order
{
    public class OrderDto
    {
        public string Id { get; set; }

        public decimal TotalAmount { get; set; }

        //[Required]
        //public PaymentStatus PaymentStatus { get; set; }

        //[Required]
        //public OrderStatus Status { get; set; }

        public string UserId { get; set; }
    }
}
