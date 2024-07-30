using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    public class Billing : BaseEntity
    {
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }
}
