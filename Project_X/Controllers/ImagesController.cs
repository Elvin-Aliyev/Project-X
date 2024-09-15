using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_X.Models.Domain;
using Project_X.Models.DTO;

namespace Project_X.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IBImageRepository bImageRepository;
        public ImagesController(IBImageRepository bImageRepository)
        {
            this.bImageRepository = bImageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string filename, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var blogimage = new BImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = filename,
                    Title = title,
                    CreatedDate = DateTime.UtcNow,
                };
                blogimage = await bImageRepository.UploadImageAsync(file, blogimage);
                //convert to dto
                var reponse = new BImageDto
                {
                    Id = blogimage.Id,
                    Title = blogimage.Title,
                    CreatedDate = blogimage.CreatedDate,
                    FileExtension = blogimage.FileExtension,
                    FileName = blogimage.FileName,
                    Url = blogimage.Url
                };
                return Ok(blogimage);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowextension = new string[] { ".jpg", ".jpeg", ".png" };
            if ( !allowextension.Contains(Path.GetExtension(file.FileName).ToLower()) )
            {
                ModelState.AddModelError("file","unsupported file type");
            }
            if(file.Length > 10485768)
            {
                ModelState.AddModelError("file","filesize cant be more than 10MB");
            }
        }
    }
}
