namespace PurchasePortal.Web.IRepository.Result
{
    public interface IDataResult<T> : IResult where T : class
    {
        T Result { get; }
    }
}
