using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<FavoritItem> FavoritItems { get; set; } 
        public virtual ICollection<CartItem> CartItems { get; set; } 

        public string FullName => $"{FirstName} {LastName}";
        public void UpdateAddress(string street, string city, string postalCode)
        {
            Address ??= Address = new Address();
            

            Address.Street = street;
            Address.City = city;
            Address.PostalCode = postalCode;
        }
    }
}
