using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public class SQLBlogRepository : IBlogRepository
    {
        private readonly DSEMLDbContext dbContext;
        public SQLBlogRepository(DSEMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Blog> CreateAsync(Blog blg)
        {
            await dbContext.Blogs.AddAsync(blg);
            await dbContext.SaveChangesAsync();
            return blg;
        }

        public async Task<Blog?> DeleteAsync(Guid id)
        {
            var dbblog = await dbContext.Blogs.FindAsync(id);
            if (dbblog == null) return null;
            dbContext.Blogs.Remove(dbblog);
            dbContext.SaveChanges();
            return dbblog;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await dbContext.Blogs.Include("Category").ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await dbContext.Blogs.Include("Category").FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Blog?> UpdateAsync(Guid id, Blog blg)
        {
            var dbblg = await dbContext.Blogs.FindAsync(id);
            if (dbblg == null) return null;
            //dbblg.Category = blg.Category;
            dbblg.Title = blg.Title;
            dbblg.Description = blg.Description;
            dbblg.Content = blg.Content;
            //dbblg.CreatedAt = blg.CreatedAt;
            dbblg.UpdatedAt = DateTime.UtcNow;
            dbblg.IsPublished = blg.IsPublished;
            dbblg.CategoryId = blg.CategoryId;


            await dbContext.SaveChangesAsync();
            return dbblg;
        }
    }
}
