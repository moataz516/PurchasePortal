using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Models;

namespace PurchasePortal.Web.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();
            SeedCategories(context);
            SeedProducts(context);
            SeedUsers(userManager, roleManager);
        }

        private static void SeedCategories(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and gadgets" },
                    new Category { Name = "Clothing", Description = "Apparel and fashion items" },
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void SeedProducts(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                var electronicsCategory = context.Categories.First(c => c.Name == "Electronics");

                var products = new[]
                {
                    new Product { Name = "Smartphone", Slug="Smartphone", Description = "High-end smartphone", Price = 999.99m, StockQuantity = 100, CategoryId = electronicsCategory.Id },
                    new Product { Name = "Laptop", Slug= "Laptop"  , Description = "Powerful laptop for professionals", Price = 1499.99m, StockQuantity = 50, CategoryId = electronicsCategory.Id },
                    // Add more products as needed
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            if (!context.PromotionCategories.Any())
            {
                var promotionCategories = new[]
                {
                    new PromotionCategory{Name="Hot Deals"},
                    new PromotionCategory{Name="Special Offers"},
                    new PromotionCategory{Name="Featured Products"},
                    new PromotionCategory{Name="New Arrivals"},
                    new PromotionCategory{Name="Seasonal Sales"},
                    new PromotionCategory{Name="Trending Now"},
                };
                context.PromotionCategories.AddRange(promotionCategories);
                context.SaveChanges();
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                // Ensure roles exist
                var roles = new[]
                {
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "User" }
            // Add more roles as needed
        };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role.Name).Result)
                    {
                        var r = roleManager.CreateAsync(role).Result;
                        if (!r.Succeeded)
                        {
                            throw new Exception($"Failed to create role {role.Name}: {r.Errors.First().Description}");
                        }
                    }
                }

                // Create a user and assign roles
                var user = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true // Optionally confirm the email
                };

                var result = userManager.CreateAsync(user, "Password123!").Result;

                if (result.Succeeded)
                {
                    // Assign roles to the user
                    foreach (var role in roles)
                    {
                        if (!userManager.IsInRoleAsync(user, role.Name).Result)
                        {
                            userManager.AddToRoleAsync(user, role.Name).Wait();
                        }
                    }
                }
                else
                {
                    throw new Exception($"Failed to create user: {result.Errors.First().Description}");
                }
            }
        }


    }

}
