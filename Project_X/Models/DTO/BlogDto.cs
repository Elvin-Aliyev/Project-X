namespace Project_X.Models.DTO
{
    public class BlogDto
    {
        public string Title { get; set; }
        public string Description { get; set; }     //sadece view -da kicik bir yazi ucun lazimdir
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsPublished { get; set; } = false;


        //public Guid CategoryId { get; set; }
        public BCategoryDto Category { get; set; }
    }
}
