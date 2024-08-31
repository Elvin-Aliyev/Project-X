using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public class SQLBCategoryRepository : IBCategoryRepository
    {
        private readonly DSEMLDbContext dbContext;
        public SQLBCategoryRepository(DSEMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<BCategory>> GetAllAsync()
        {
            return await dbContext.BCategories.ToListAsync();
        }

        public async Task<BCategory?> GetByIdAsync(Guid id)
        {
            return await dbContext.BCategories.FindAsync(id);
        }
        public async Task<BCategory> CreateAsync(BCategory faq)
        {
            await dbContext.BCategories.AddAsync(faq);
            await dbContext.SaveChangesAsync();
            return faq;
        }

        public async Task<BCategory?> UpdateAsync(Guid id, BCategory faq)
        {
            var dbcat = await dbContext.BCategories.FindAsync(id);
            if (dbcat == null) return null;
            dbcat.Description = faq.Description;
            dbcat.Name = faq.Name;
            await dbContext.SaveChangesAsync();
            return dbcat;
        }

        public async Task<BCategory?> DeleteAsync(Guid id)
        {
            var dbcat = await dbContext.BCategories.FindAsync(id);
            if (dbcat == null) return null; 
            dbContext.BCategories.Remove(dbcat);
            dbContext.SaveChanges();
            return dbcat;
        }
    }
}
