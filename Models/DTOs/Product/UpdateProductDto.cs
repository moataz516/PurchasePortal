using PurchasePortal.Web.Models.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models.DTOs.Product
{
    public class UpdateProductDto
    {
        [Required]
        public string Id { get; set; }
            
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; } = 1;

        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Stock Quantity must be between 1 and 10,000.")]
        public int StockQuantity { get; set; } = 1;

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }

        public List<CategoryDto> CategoriesDto { get; set; } 
    }
}

