using System.ComponentModel.DataAnnotations;

namespace Plaza_DSML.Models.DTOs
{
    public class SponsorDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class UpdateSponsorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
