using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Data;
using Plaza_DSML.Models;
using System.Diagnostics;

namespace Plaza_DSML.Repositories
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(Guid id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog?> UpdateAsync(/*Guid id,*/ Blog blog);
        Task<Blog?> DeleteAsync(/*Guid id*/  Blog blog);
    }

    public class BlogRepository(AppDbContext dbContext) : IBlogRepository
    {
        public async Task<Blog> CreateAsync(Blog blog)
        {
            await dbContext.Blogs.AddAsync(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog?> DeleteAsync(/*Guid id*/ Blog blog)
        {
            /*var dbblog = await dbContext.Blogs.FindAsync(id);
            if (dbblog == null) return null;*/
            dbContext.Blogs.Remove(blog);
            dbContext.SaveChanges();
            return blog;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await dbContext.Blogs.ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await dbContext.Blogs.FindAsync(id);
        }

        public async Task<Blog?> UpdateAsync(/*Guid id,*/ Blog blog)
        {
            /*var dbblog = await dbContext.Blogs.FindAsync(blog.Id);
            if (dbblog == null) return null;
            dbblog.Title = blog.Title;
            dbblog.Content = blog.Content;
            dbblog.IsPublished = blog.IsPublished;

            await dbContext.SaveChangesAsync();
            */
            dbContext.Blogs.Update(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }
    }
}
