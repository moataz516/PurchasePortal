using PurchasePortal.Web.Models;
using PurchasePortal.Web.Models.DTOs.Auth;
using PurchasePortal.Web.Services.ResultService;

namespace PurchasePortal.Web.Repository
{
    public interface IAuthService
    {
        Task<Result> SignInAsync(LoginDto model);
        Task<Result> SignUpAsync(RegisterDto model);
        Task SignOutAsync();
    }
}
