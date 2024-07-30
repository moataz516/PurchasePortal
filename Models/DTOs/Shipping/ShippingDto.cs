using PurchasePortal.Web.Models.DTOs.Order;

namespace PurchasePortal.Web.Models.DTOs.Shipping
{
    public class ShippingDto
    {
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
