
using PurchasePortal.Web.Models.Error;

namespace PurchasePortal.Web.Services.ResultService
{
    public class Result : IRepository.Result.IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ErrorDetail Error { get; set; }


        public Result()
        {
            IsSuccess = true;
            Error = new ErrorDetail();
        }
    }
}
