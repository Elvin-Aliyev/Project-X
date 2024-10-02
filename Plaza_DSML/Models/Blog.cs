namespace Plaza_DSML.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPublished { get; set; }
        public string BlogImage { get; set; }
    }
}
