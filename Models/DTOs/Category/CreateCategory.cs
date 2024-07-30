using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models.DTOs.Category
{
    public class CreateCategory
    {
        [MinLength(2)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
