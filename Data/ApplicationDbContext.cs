using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Extensions;
using PurchasePortal.Web.Models;
using static PurchasePortal.Web.Extensions.ModelBuilderExtensions;

namespace PurchasePortal.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PromotionCategory> PromotionCategories { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<FavoritItem> FavoritItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Billing> Billings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>(OrderConfiguration.ConfigureOrder);
            builder.Entity<OrderItem>(OrderItemConfiguration.ConfigureOrderItem);
            builder.Entity<Product>(ProductConfiguration.ConfigureProduct);
            builder.Entity<Category>(CategoryConfiguration.ConfigureCategory);
            builder.Entity<Payment>(PaymentConfiguration.ConfigurePayment);
            builder.Entity<Address>(AddressConfiguration.ConfigureAddress);
            builder.Entity<ApplicationUser>(ApplicationUserConfiguration.ConfigureApplicationUser);
            builder.Entity<ProductPromotion>(ProductPromotionConfigure.ConfigureProductPromotion);
            builder.Entity<FavoritItem>(FavoriteItemConfiguration.ConfigureFavoritItem);
            builder.Entity<CartItem>(CartItemConfiguration.ConfigureCartItem);
            builder.ConfigureIdentityTables();

        }

    }
}
