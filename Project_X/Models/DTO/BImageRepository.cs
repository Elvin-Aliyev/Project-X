using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Models.Domain;

namespace Project_X.Models.DTO
{
    public class BImageRepository : IBImageRepository
    {
        private readonly IWebHostEnvironment webHostBuilder;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DSEMLDbContext dbContext;
        public BImageRepository(IWebHostEnvironment webHostBuilder, IHttpContextAccessor httpContextAccessor, DSEMLDbContext dbContext)
        {
            this.webHostBuilder = webHostBuilder;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<BImage> UploadImageAsync(IFormFile file, BImage blogimage)
        {
            var localPath = Path.Combine(webHostBuilder.ContentRootPath,"Images",$"{blogimage.FileName}{blogimage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Image/{blogimage.FileName}{blogimage.FileExtension}";
            blogimage.Url = urlPath;
            await dbContext.Bimages.AddAsync(blogimage);
            await dbContext.SaveChangesAsync();
            return blogimage;
        }
    }
}
