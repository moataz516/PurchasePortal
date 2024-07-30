using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    [Index(nameof(UserId))]
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
