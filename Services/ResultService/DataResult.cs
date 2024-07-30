using PurchasePortal.Web.IRepository.Result;
using PurchasePortal.Web.Models.Error;

namespace PurchasePortal.Web.Services.ResultService
{
    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T Result { get; set; }

    }
}
