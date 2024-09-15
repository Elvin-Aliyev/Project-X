using Microsoft.EntityFrameworkCore;
using Project_X.Models.Domain;

namespace Project_X.Data
{
    public class DSEMLDbContext:DbContext
    {
        public DSEMLDbContext(DbContextOptions context):base( context)
        {
            
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.LogTo(Console.WriteLine);*/
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Sponsors> Sponsors { get; set; }
        public DbSet<BCategory> BCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<BImage> Bimages { get; set; }

    }
}
