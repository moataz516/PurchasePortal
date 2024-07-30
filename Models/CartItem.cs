using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    public class CartItem : BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
