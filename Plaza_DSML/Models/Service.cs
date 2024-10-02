namespace Plaza_DSML.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPublished { get; set; }
        public string ServiceImage { get; set; }
    }
}
