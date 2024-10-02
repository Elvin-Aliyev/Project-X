namespace Plaza_DSML.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
