using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using PurchasePortal.Web.Data.Enum;
using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Auth;
using PurchasePortal.Web.Models.Error;
using PurchasePortal.Web.Repository;
using PurchasePortal.Web.Services.ResultService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PurchasePortal.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ProductService> _logger;

        public AuthService(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ProductService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<Result> SignInAsync(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
;
                    return new Result
                    {
                        IsSuccess = false,
                        Error = new ErrorDetail { ErrorMessage = "Invalid credintial" }
                    };
                }
                var isLogedIn = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                return isLogedIn.Succeeded
                    ? new Result { IsSuccess = true, Message = "User logged in successfully." }
                    : isLogedIn.IsLockedOut
                        ? new Result { IsSuccess = false, Error = new ErrorDetail { ErrorMessage = "Account locked out. Try again later." } }
                        : new Result { IsSuccess = false, Error = new ErrorDetail { ErrorMessage = "Something went wrong!" } };
            }
            catch (Exception ex)
            {
                var result = new Result
                {
                    IsSuccess = false,
                    Error = new ErrorDetail(ex.InnerException.Message, 404, ex.StackTrace),
                };
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
                return result;
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        public async Task<Result> SignUpAsync(RegisterDto model)
        {
            try
            {
                if(await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Error = new ErrorDetail { ErrorMessage = "User are exist" }
                    };
                }
                var newUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };
                var createNewUser = await _userManager.CreateAsync(newUser, model.Password);
                if(createNewUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User.ToString());
                    return new Result { Message = "User Created Succesfully"};
                }
                return new Result
                {
                    IsSuccess = false,
                    Error = new ErrorDetail { ErrorMessage = "Something went wrong!" }
                };
            }
            catch (Exception ex)
            {
                var result = new Result
                {
                    IsSuccess = false,
                    Error = new ErrorDetail(ex.InnerException.Message, 404, ex.StackTrace),
                };
                _logger.LogError($"Error details: {result.Error.ErrorMessage} ', Code: {result.Error.ErrorCode}, StackTrace: {result.Error.StackTrace},");
                return result;
            }
        }
    }
}
