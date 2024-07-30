using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Helpers;
using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Models.DTOs.Order;
using PurchasePortal.Web.Models.DTOs.Shipping;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services;
using System.Security.Claims;

namespace PurchasePortal.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public OrderController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet("Checkout")]
        public async Task<IActionResult> CheckOut()
        {
            var userId = GetCurrentUserId();
            var cartItems = await _cartService.GetActiveCartItemsByUserAsync(userId);
            var totalPrice = cartItems.Sum(s => s.ProductPrice * s.Quantity);

            var checkoutSession = new CheckoutSessionDto
            {
                CartItems = cartItems.ToList(),
                TotalPrice = totalPrice,
            };
            HttpContext.Session.SetObject("CheckoutSession", checkoutSession);

            return View("Shipping");
        }

        [HttpPost("AddShipping")]
        public async Task<IActionResult> AddShipping(ShippingDto shipping)
        {
            var checkoutSession = HttpContext.Session.GetObject<CheckoutSessionDto>("CheckoutSession");
            if (checkoutSession != null)
            {
                checkoutSession.ShippingAddress = shipping;
                HttpContext.Session.SetObject("CheckoutSession", checkoutSession);
            }
            else
            {
                // Handle the case when session is null
                return RedirectToAction("Error"); // Example redirection for error handling
            }
            var model = new CheckoutSessionDto { CartItems = checkoutSession.CartItems.ToList() ,ShippingAddress = shipping, TotalPrice = checkoutSession.TotalPrice };
            return View("ConfirmOrder", model);
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var checkoutSession = HttpContext.Session.GetObject<CheckoutSessionDto>("CheckoutSession");
            if (checkoutSession == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            var userId = GetCurrentUserId();
            var cartItems = checkoutSession.CartItems.ToList();
            var shippingAddress = checkoutSession.ShippingAddress;

            await _orderService.CreateOrderAsync(userId, cartItems, shippingAddress);
            await _cartService.ClearCartAsync(userId);

            HttpContext.Session.Remove("CheckoutSession"); 
            TempData["SuccessMessage"] = "Order Succedded";

            return RedirectToAction("Index", "Home");
        }
    }
}
