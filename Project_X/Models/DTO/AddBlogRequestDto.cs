using System.ComponentModel.DataAnnotations;

namespace Project_X.Models.DTO
{
    public class AddBlogRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title has to be minimum of 5 character")]
        [MaxLength(150, ErrorMessage = "Title has to be maximum of 150 character")]
        public string Title { get; set; }
        [Required]
        [MinLength(50, ErrorMessage = "Description has to be minimum of 50 character")]
        [MaxLength(250, ErrorMessage = "Description has to be maximum of 150 character")]
        public string Description { get; set; }     //sadece view -da kicik bir yazi ucun lazimdir
        [Required]
        [MinLength(150, ErrorMessage = "Content has to be minimum of 50 character")]
        [MaxLength(500, ErrorMessage = "Content has to be maximum of 150 character")]
        public string Content { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        [Required]
        public bool IsPublished { get; set; } = false;
        public Guid CategoryId { get; set; }
        //public BCategoryDto Category { get; set; }
    }
}
