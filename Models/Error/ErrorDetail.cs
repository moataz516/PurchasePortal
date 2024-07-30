using System.Diagnostics;

namespace PurchasePortal.Web.Models.Error
{
    public class ErrorDetail
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string StackTrace { get; set; }
        public DateTime TimeStamp { get; set; }

        public ErrorDetail() {
            TimeStamp = DateTime.UtcNow;
        }
        public ErrorDetail(string errorMessage, int errorCode = 500 , string stackTrace = null)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            StackTrace = stackTrace;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
