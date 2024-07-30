using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models
{
    [Index(nameof(Name))]
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
