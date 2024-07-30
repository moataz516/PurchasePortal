using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Data.Enum;
using PurchasePortal.Web.IRepository;
using PurchasePortal.Web.Models.Common;
using PurchasePortal.Web.Models.DTOs.Cart;
using System.ComponentModel.DataAnnotations;

namespace PurchasePortal.Web.Models
{

    [Index(nameof(UserId))]
    [Index(nameof(OrderDate))]
    public class Order : BaseEntity
    {

        public Order()
        {
            OrderDate = DateTime.UtcNow;
            //PaymentStatus = PaymentStatus.Pending;
            //Status = OrderStatus.Pending;
        }


        public event EventHandler<OrderStatusChangedEventArgs> OrderStatusChanged;
        private OrderStatus status;


        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a positive number")]
        public decimal TotalAmount { get; set; }
        [Required]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        public OrderStatus Status
        {
            set
            {
                if (status != value)
                {
                    status = value;
                    OnOrderStatusChanged(new OrderStatusChangedEventArgs(this, status));
                }
            }
            get => status;
        }
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual Billing Billing { get; set; }


        //public void AddOrderItem(OrderItem item)
        //{
        //    OrderItems.Add(item);
        //    TotalAmount += item.Price * item.Quantity;
        //}

        //public void RemoveOrderItem(OrderItem item)
        //{
        //    if (OrderItems.Remove(item))
        //    {
        //        TotalAmount -= item.Price * item.Quantity;
        //    }
        //}

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
        }




        protected virtual void OnOrderStatusChanged(OrderStatusChangedEventArgs e)
        {
            OrderStatusChanged?.Invoke(this, e);
        }
    }

    public class OrderStatusChangedEventArgs : EventArgs
    {
        public Order Order { get; }
        public OrderStatus NewStatus { get; }

        public OrderStatusChangedEventArgs(Order order, OrderStatus newStatus)
        {
            Order = order;
            NewStatus = newStatus;
        }



        //public async Task AddOrderItems(IEnumerable<CartItemDto> cartItems, IProductRepository productRepository)
        //{

        //    foreach (var item in cartItems)
        //    {
        //        var product = await productRepository.GetByIdAsync(item.ProductId);
        //        if (product == null)
        //        {
        //            throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
        //        }

        //        // Update stock before creating the order item

        //            if (item.Quantity < 0 && product.StockQuantity + item.Quantity < 0)
        //            {
        //                throw new InvalidOperationException("Insufficient stock to complete the operation.");
        //            }

        //            product.StockQuantity -= item.Quantity;
        //            product.QuantitySold += item.Quantity;
                

        //        var orderItem = new OrderItem
        //        {
        //            ProductId = item.ProductId,
        //            Quantity = item.Quantity,
        //            Price = item.ProductPrice,
        //            Product = product,

        //        };

        //        Order.OrderItems.Add(orderItem);
        //    }
        //}   
    }

}
