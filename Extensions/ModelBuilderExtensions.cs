using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Models;
using System.Reflection.Emit;

namespace PurchasePortal.Web.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureIdentityTables(this ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

        public static class OrderConfiguration
        {
            public static void ConfigureOrder(EntityTypeBuilder<Order> entity)
            {
                entity.HasKey(o => o.Id);

                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Payments)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.Shipping)
                    .WithOne(s => s.Order)
                    .HasForeignKey<Shipping>(s => s.OrderId);

                entity.HasOne(o => o.Billing)
                    .WithOne(b => b.Order)
                    .HasForeignKey<Billing>(b => b.OrderId);

                entity.HasIndex(o => new { o.UserId, o.OrderDate });
            }
        }


    }


    public static class OrderItemConfiguration
    {
        public static void ConfigureOrderItem(EntityTypeBuilder<OrderItem> entity)
        {
            entity.HasKey(oi => oi.Id);

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(oi => !oi.IsActive);


        }
    }

    public static class ProductConfiguration
    {
        public static void ConfigureProduct(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.Name).IsUnique();
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.HasIndex(p => p.CategoryId);

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public static class ProductPromotionConfigure
    { 
    public static void ConfigureProductPromotion(EntityTypeBuilder<ProductPromotion> entity)
    {
        entity.HasKey(pp => new { pp.ProductId, pp.PromotionCategoryId });

        entity.HasOne(pp => pp.Product)
        .WithMany(p => p.ProductPromotions)
        .HasForeignKey(pp => pp.ProductId);

        entity.HasOne(pp => pp.PromotionCategory)
        .WithMany(pc => pc.ProductPromotions)
        .HasForeignKey(pp => pp.PromotionCategoryId);
    }
}
    public static class CategoryConfiguration
        {
            public static void ConfigureCategory(EntityTypeBuilder<Category> entity)
            {
                entity.HasKey(c => c.Id);
                entity.HasIndex(c => c.Name).IsUnique();

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(500);
        }
    }

    public static class FavoriteItemConfiguration
    {
        public static void ConfigureFavoritItem(EntityTypeBuilder<FavoritItem> entity)
        {

            entity.HasIndex(f => new { f.UserId, f.ProductId })
            .IsUnique();

            entity.HasOne(f => f.User)
                .WithMany(p => p.FavoritItems)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Product)
                .WithMany(p => p.FavoritItems)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //entity.HasQueryFilter(fi => !fi.isActive);

        }
    }


    public static class CartItemConfiguration
    {
        public static void ConfigureCartItem(EntityTypeBuilder<CartItem> entity)
        {

            entity.HasIndex(f => new { f.UserId, f.ProductId })
            .IsUnique();

            entity.HasOne(f => f.User)
                .WithMany(p => p.CartItems)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //entity.HasQueryFilter(ci => !ci.IsActive);

        }
    }


    public static class PaymentConfiguration
        {
            public static void ConfigurePayment(EntityTypeBuilder<Payment> entity)
            {
                entity.HasKey(p => p.Id);

                entity.HasIndex(p => p.OrderId);

                entity.Property(p => p.Amount)
                    .HasColumnType("decimal(18, 2)");


                entity.HasOne(p => p.Order)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public static class AddressConfiguration
        {
            public static void ConfigureAddress(EntityTypeBuilder<Address> entity)
            {
                entity.HasKey(a => a.Id);

                entity.HasIndex(a => new { a.UserId, a.PostalCode }).IsUnique();

                entity.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(a => a.User)
                    .WithOne(u => u.Address)
                    .HasForeignKey<Address>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public static class ApplicationUserConfiguration
        {
            public static void ConfigureApplicationUser(EntityTypeBuilder<ApplicationUser> entity)
            {
                entity.HasKey(u => u.Id);

                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(a => a.FavoritItems)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
        }
    }


