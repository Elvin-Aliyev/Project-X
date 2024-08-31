namespace Project_X.Models.Domain
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }     //sadece view -da kicik bir yazi ucun lazimdir
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsPublished { get; set; } = false;


        public Guid CategoryId { get; set; }
        public BCategory Category { get; set; }
    }
}
