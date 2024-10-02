using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Models;

namespace Plaza_DSML.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
