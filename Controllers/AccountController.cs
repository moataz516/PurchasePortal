using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Auth;
using PurchasePortal.Web.Repository;

namespace PurchasePortal.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {

            _authService = authService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.SignUpAsync(model);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Login");
            }
            TempData["ErrorMessage"] = result.Error.ErrorMessage;
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _authService.SignInAsync(model);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = result.Error.ErrorMessage;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
