namespace PurchasePortal.Web.Models.DTOs.Search
{
    public class SearchDto
    {
        public string catId { get; set; } = string.Empty;
        public string sortBy { get; set; }
        public string filter { get; set; } = string.Empty;
        public bool desc { get; set; } = false;
    }
}
