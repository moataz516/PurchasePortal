namespace PurchasePortal.Web.IRepository.Result
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
