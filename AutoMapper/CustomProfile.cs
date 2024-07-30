using AutoMapper;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Models.DTOs.Category;
using PurchasePortal.Web.Models.DTOs.Favorite;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.PromotionCategory;
using PurchasePortal.Web.Models.DTOs.Shipping;

namespace PurchasePortal.Web.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
           CreateMap<Category, CategoryDto>().ReverseMap();
           CreateMap<Category, CreateCategory>().ReverseMap();
           CreateMap<Category, UpdateCategoryDto>().ReverseMap();
           CreateMap<Category, CategoryWithProductsDto>().ReverseMap();



           CreateMap<Product, ProductDto>().ReverseMap();
           CreateMap<Product, CreateProduct>().ReverseMap();
           CreateMap<Product, UpdateProductDto>().ReverseMap();
           CreateMap<Product, ProductDetailsDto>().ReverseMap();
           CreateMap<Product, DeleteProductDto>().ReverseMap();
           CreateMap<Product, ProductIndexDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();


            CreateMap<PromotionCategory, PromotionCategoryDto >().ReverseMap();
            CreateMap<PromotionCategory, UpdatePromotionCategoryDto>().ReverseMap();
            CreateMap<PromotionCategory, CreatePromotionCategoryDto >().ReverseMap();
            CreateMap<PromotionCategory, PromotionCategoryHomeDto >()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductPromotions.Select(pp => pp.Product)))
                .ReverseMap();



            CreateMap<FavoritItem, FavoritDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Product.StockQuantity))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));


            CreateMap<Shipping, ShippingDto>().ReverseMap();

            CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
            .ReverseMap();

        }
    }
}
