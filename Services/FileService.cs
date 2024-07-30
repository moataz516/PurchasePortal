using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurchasePortal.Web.Models;
using PurchasePortal.Web.Repository;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Linq;

namespace PurchasePortal.Web.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string uploadPath)
        {
            
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, uploadPath);
            var uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            
            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(800, 600)
                }));
                var jpegEncoder = new JpegEncoder
                {
                    Quality = 75
                };
                await image.SaveAsJpegAsync(filePath, jpegEncoder);
            }
            return "/" + uploadPath + "/" + uniqueFileName;

            //var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, uploadPath);
            //var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            //var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}

            //return "/" + uploadPath + "/" + uniqueFileName;
        }

        public void DeleteFile(string filePath)
        {
            if (filePath != null)
            {
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}
/*
if (imageFile != null && imageFile.Length > 0)
{
    // Validate file size (e.g., max 5 MB)
    if (imageFile.Length > 5 * 1024 * 1024)
    {
        ModelState.AddModelError("ImagePath", "File size cannot exceed 5 MB.");
        ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View(product);
    }

    // Validate file type
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

    if (!allowedExtensions.Contains(extension))
    {
        ModelState.AddModelError("ImagePath", "Invalid file type. Only JPG, JPEG, PNG, and GIF files are allowed.");
        ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View(product);
    }

    // Generate unique file name and save the file
    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
        await imageFile.CopyToAsync(fileStream);
    }

    product.ImagePath = "/uploads/" + uniqueFileName;
}
*/