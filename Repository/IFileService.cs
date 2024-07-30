namespace PurchasePortal.Web.Repository
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string uploadPath);
        void DeleteFile(string filePath);
    }
}
