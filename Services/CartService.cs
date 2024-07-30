using AutoMapper;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Repository;

namespace PurchasePortal.Web.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddToCartAsync(AddToCartDto request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null || product.StockQuantity <= 0)
            {
                throw new InvalidOperationException("Product is not available.");
            }

            var cartItem = await _cartRepository.GetActiveCartItemAsync(request.UserId, request.ProductId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = 1,
                    IsActive = true
                };
                await _cartRepository.AddToCartAsync(cartItem);
            } 
            else
            {
                if(cartItem.IsActive == false) {
                    cartItem.IsActive = true;
                }
                else { 
                cartItem.Quantity += 1;
                }
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
        }

        public async Task MarkCartItemAsInactiveAsync(MarkCartItemAsInactiveDto request)
        {
            var cartItem = await _cartRepository.GetActiveCartItemAsync(request.UserId, request.ProductId);
            if (cartItem != null)
            {
                cartItem.IsActive = false;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
        }

        public async Task UpdateCartItemQuantityAsync(UpdateCartItemQuantityDto request)
        {
            if (request.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(request.Quantity));
            }

            var cartItem = await _cartRepository.GetActiveCartItemAsync(request.UserId, request.ProductId);
            if (cartItem != null)
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null || product.StockQuantity < request.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock for the product.");
                }

                cartItem.Quantity = request.Quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _cartRepository.GetActiveCartItemsByUserAsync(userId);
            foreach (var cartItem in cartItems)
            {
                cartItem.IsActive = false;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
        }

        public async Task<IEnumerable<CartItemDto>> GetActiveCartItemsByUserAsync(string userId)
        {
            var cartItems = await _cartRepository.GetActiveCartItemsByUserAsync(userId);
            return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        }

        public Task<int> GetTotalCartItemsByUserAsync(string userId)
        {
            return _cartRepository.GetTotalCartItemByUserAsync(userId);
        }
    }
}
