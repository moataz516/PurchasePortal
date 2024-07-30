using AutoMapper;
using PurchasePortal.Web.Data.Enum;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Models.DTOs.Shipping;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.IService;

namespace PurchasePortal.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<string> CreateOrderAsync(string userId, List<CartItemDto> cartItems, ShippingDto shippingAddress)
        {
            var order = new Order
            {
                UserId = userId,
                TotalAmount = cartItems.Sum(item => item.ProductPrice * item.Quantity),
                PaymentStatus = PaymentStatus.Pending,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.UtcNow,
                Shipping = _mapper.Map<Shipping>(shippingAddress),
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.ProductPrice
                }).ToList(),
            };

            foreach (var item in cartItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
                }


                if (item.Quantity < 0 && product.StockQuantity + item.Quantity < 0)
                {
                    throw new InvalidOperationException("Insufficient stock to complete the operation.");
                }
                product.StockQuantity -= item.Quantity;
                product.QuantitySold += item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

                return await _orderRepository.CreateOrderAsync(order);

        }
    }
}
