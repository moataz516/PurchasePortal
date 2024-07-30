using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models
{
    public class FavoritItem : BaseEntity
    {
        public string UserId { get; set; }
        public virtual  ApplicationUser User { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public bool isActive { get; set; } = true;
    }
}
