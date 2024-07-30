namespace PurchasePortal.Web.Models.Common
{
    public abstract  class BaseEntity
    {

        public BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }


        public string Id { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
