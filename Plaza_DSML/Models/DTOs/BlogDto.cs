using System.ComponentModel.DataAnnotations;

namespace Plaza_DSML.Models.DTOs
{
    public class BlogDto
    {
        [Required,MaxLength(255)]
        public string Title { get; set; }
        [Required,MaxLength(255)]
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        /*[Required,MaxLength(255)]
        public string BlogImage { get; set; }*/
        public IFormFile ImageFile { get; set; }
    }
    public class UpdateBlogDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string BlogImage { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
