using Project_X.Models.Domain;

namespace Project_X.Models.DTO
{
    public interface IBImageRepository
    {
        Task<BImage> UploadImageAsync(IFormFile file,BImage image);
    }
}
