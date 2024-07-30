using PurchasePortal.Web.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data.Enum;

namespace PurchasePortal.Web.Models
{

    [Index(nameof(OrderId))]
    public class Payment : BaseEntity
    {
        public Payment()
        {
            PaymentDate = DateTime.UtcNow;
        }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

    }


}
