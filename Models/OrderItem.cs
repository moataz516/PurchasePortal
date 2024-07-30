using PurchasePortal.Web.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models
{
    public class OrderItem : BaseEntity
    {

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required]

        public bool IsActive { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
