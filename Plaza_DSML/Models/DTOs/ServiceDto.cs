using System.ComponentModel.DataAnnotations;

namespace Plaza_DSML.Models.DTOs
{
    public class ServiceDto
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required, MaxLength(255)]
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class UpdateServiceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string ServiceImage { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
