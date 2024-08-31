using System.ComponentModel.DataAnnotations;

namespace Project_X.Models.DTO
{
    public class BCategoryDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Category Name has to be minimum of 5 character")]
        [MaxLength(150, ErrorMessage = "Category Name has to be maximum of 150 character")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Category Description has to be minimum of 5 character")]
        [MaxLength(150, ErrorMessage = "Category Description has to be maximum of 150 character")]
        public string Description { get; set; }
    }
}
