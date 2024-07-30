using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Helpers;
using PurchasePortal.Web.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models
{
    [Index(nameof(Name))]
    [Index(nameof(CategoryId))]
    [Index(nameof(Slug))]
    public class Product : BaseEntity
    {
        private string _name;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get { return _name; } set { _name = value; GenerateSlug(); } }
        [Required]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a positive number.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Image {  get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Discount { get; set; }
        public int QuantitySold { get; set; }


        public virtual ICollection<ProductPromotion> ProductPromotions { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<FavoritItem> FavoritItems { get; set; }
        public virtual ICollection<CartItem>  CartItems { get; set; } 


        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && StockQuantity + quantity < 0)
            {
                throw new InvalidOperationException("Insufficient stock to complete the operation.");
            }

            StockQuantity += quantity;
        }

        public void GenerateSlug()
        {
            Slug = Name.Slugify();
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice), "Price must be greater than zero.");
            }

            Price = newPrice;
        }
    }
}
