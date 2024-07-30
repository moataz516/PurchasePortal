using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Models.DTOs.Cart;
using PurchasePortal.Web.Repository;
using System.Security.Claims;

namespace PurchasePortal.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpGet("GetActiveCartItems")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartService.GetActiveCartItemsByUserAsync(userId);
            var totalPrice = cartItems.Sum(item => item.ProductPrice * item.Quantity);
            var result = new CartDto
            {
                TotalPrice = totalPrice,
                CartItems = cartItems
            };

            return View(result);
        }


        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartDto  request)
        {
            if (request == null || string.IsNullOrEmpty(request.ProductId))
            {
                return BadRequest("Invalid request.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }


            try
            {
                request.UserId = userId;
                await _cartService.AddToCartAsync(request);

                var product = await _productService.GetProductByIdAsync(request.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found.");
                }
                else
                {
                    return RedirectToAction("ProductDetails", "Product", new { Slug = product.Result.Slug });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCartItemQuantity")]
        public async Task<IActionResult> UpdateCartItemQuantity(UpdateCartItemQuantityDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.ProductId) || request.Quantity <= 0)
            {
                return BadRequest("Invalid request.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                request.UserId = userId;
                await _cartService.UpdateCartItemQuantityAsync(request);
                // Ok("Cart item quantity updated.");
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("MarkCartItemAsInactive")]
        public async Task<IActionResult> MarkCartItemAsInactive(MarkCartItemAsInactiveDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.ProductId))
            {
                return BadRequest("Invalid request.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            request.UserId = userId;
            await _cartService.MarkCartItemAsInactiveAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _cartService.ClearCartAsync(userId);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("GetTotalCart")]
        public async Task<IActionResult> GetTotalCartsAsync()
        {
            var totalCart = await _cartService.GetTotalCartItemsByUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Json(new { count = totalCart });
        }
    }
}
