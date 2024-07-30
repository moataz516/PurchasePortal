using PurchasePortal.Web.Models.Common;

namespace PurchasePortal.Web.Models.DTOs.Category
{
    public class UpdateCategoryDto : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
