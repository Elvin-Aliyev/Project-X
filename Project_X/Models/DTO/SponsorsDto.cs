using System.ComponentModel.DataAnnotations;

namespace Project_X.Models.DTO
{
    public class SponsorsDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Sponsor Name has to be minimum of 3 character")]
        [MaxLength(50, ErrorMessage = "Sponsor Name has to be maximum of 50 character")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
