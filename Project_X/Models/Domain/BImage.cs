namespace Project_X.Models.Domain
{
    public class BImage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
