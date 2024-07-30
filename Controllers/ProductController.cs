using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PurchasePortal.Web.Models.DTOs.Product;
using PurchasePortal.Web.Models.DTOs.Search;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;
using System.Security.Claims;

namespace PurchasePortal.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPromotionCategoryService _promotionCats;
        private readonly IFavoriteService _favoriteService;

        public ProductController(IProductService productService, ICategoryService categoryService, IPromotionCategoryService promotionCats, IFavoriteService favoriteService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _promotionCats = promotionCats;
            _favoriteService = favoriteService;
        }

        // GET: ProductController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetProductsForAdminAsync();
            return View(products.Result);
        }


        [HttpGet("Pro")]
        public async Task<IActionResult>Pro(string name)
        {
            var result = await _productService.GetProductsByFilter(name);
            return Json(result);
        }



        [HttpGet("category/{categoryId}/products")]
        public async Task<ActionResult> GetProductsByCategory(string categoryId, string sortBy, string filter)
        {
            var result = await _productService.GetProductsByCategoryAsync(categoryId, sortBy, filter);
            if (!result.IsSuccess)
            {
                TempData["InfoMessage"] = result?.Error.ErrorMessage;
            }

            ViewData["CurrentSort"] = sortBy;
            ViewData["CurrentFilter"] = filter;

            var products = result.Result ?? new List<ProductDto>();
            return View("ProductsByCategory", products);
        }


        [HttpGet("search")]
        public async Task<ActionResult> ProductsBySearch(SearchDto search)
        {
            var results = await _productService.GetProductsBySearchAsync(search);
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewData["Categories"] = new SelectList(categories.Result, "Id", "Name");

            return View(results.Result);
                            
        }










        //[HttpGet("category/{categoryId}/products")]
        //public async Task<ActionResult> GetProductsByCategory(string categoryId, string sortBy, string filter)
        //{
        //    var result = await _productService.GetProductsByCategoryAsync(categoryId, sortBy, filter);
        //    if (!result.IsSuccess)
        //    {
        //        TempData["InfoMessage"] = result?.Error.ErrorMessage;
        //    }

        //    ViewData["CurrentSort"] = sortBy;
        //    ViewData["CurrentFilter"] = filter;

        //    var products = result.Result ?? new List<ProductDto>();
        //    return View("ProductsByCategory", products);
        //}



        //[HttpGet("search")]
        //public async Task<ActionResult> ProductBySearch(string s)
        //{
        //    var results = await _productService.GetProductsBySearchAsync(s);

        //    if (results.IsSuccess && results.Result.Any())
        //    {
        //        return View(results.Result); // Redirect to Search view with results
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index","Home"); // Stay on the current page or return to an appropriate view
        //    }
        //}

        [HttpGet("product/{slug}")]
        public async Task<ActionResult> ProductDetails(string slug)
        {
            var product = await _productService.GetProductBySlugAsync(slug, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product.Result); 
        }


        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("GetCreateProductForm")]
        public async Task<IActionResult> GetCreateProductForm()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var promotionCats = await _promotionCats.GetAllAsync();
            ViewData["Categories"] = new SelectList(categories.Result, "Id", "Name");
            ViewData["PromotionCategories"] = new SelectList(promotionCats.Result, "Id", "Name");
            return PartialView("_CreateProduct");
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {

                var result = await _productService.AddProductAsync(createProduct);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Product added successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Error.ErrorMessage;
                }
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["ErrorMessage"] = "Invalid data. Please try again.";
            return RedirectToAction(nameof(Index), "Home");
        }

        // GET: ProductController/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}



        [Authorize(Roles = "Admin")]
        [HttpGet("GetProductForUpdate/{id}")]
        public async Task<IActionResult> GetProductForUpdate(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["Categories"] = new SelectList(categories.Result, "Id", "Name");

            var updateProductDto = new UpdateProductDto
            {
                Id = product.Result.Id,
                Name = product.Result.Name,
                Description = product.Result.Description,
                Price = product.Result.Price,
                StockQuantity = product.Result.StockQuantity,
                CategoryId = product.Result.CategoryId,
            };

            return PartialView("_UpdateProduct", updateProductDto);
        }


        // POST: ProductController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateProductDto product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProductAsync(product);
                if (!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result?.Error.ErrorMessage;
                }
                else
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            TempData["ErrorMessage"] = "Invalid data. Please try again.";
            return RedirectToAction(nameof(Index), "Home");

        }

        // GET: ProductController/Delete/5
        [HttpGet("deleteProduct/{id}")]
        public async Task<ActionResult> DeleteModal(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product.Result == null) {
                return RedirectToAction(nameof(Index), "Home");
            }
            var result = new DeleteProductDto
            {
                Id = id,
                Name = product.Result.Name,
            };
            return PartialView("_DeleteProduct", result);
        }





        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "Product is not found";
                    return RedirectToAction(nameof(Index), "Home");

                }
                var deleteProduct = await _productService.DeleteProductAsync(id);
                if (deleteProduct.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Product deleted successfully.";
                    return RedirectToAction(nameof(Index), "Home");
                }
                TempData["ErrorMessage"] = "Product Error";
                return RedirectToAction(nameof(Index), "Home");

            }
            catch
            {
                return View();
            }
        }
    }
}
