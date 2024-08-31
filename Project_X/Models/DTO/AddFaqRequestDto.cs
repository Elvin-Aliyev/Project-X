using System.ComponentModel.DataAnnotations;

namespace Project_X.Models.DTO
{
    public class AddFaqRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Question has to be minimum of 5 character")]
        [MaxLength(250, ErrorMessage = "Question has to be maximum of 250 character")]
        public string Question { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Answer has to be minimum of 5 character")]
        [MaxLength(500, ErrorMessage = "Answer has to be maximum of 500 character")]
        public string Answer { get; set; }
    }
}
