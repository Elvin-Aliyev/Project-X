using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public class SQLFaqRepository : IFaqRepository
    {
        public DSEMLDbContext dbContext { get; set; }
        public SQLFaqRepository(DSEMLDbContext dbContext)
        {
                this.dbContext = dbContext;
        }
        public async Task<List<Faq>> GetAllAsync()
        {
            return await dbContext.Faqs.ToListAsync();
        }

        public async Task<Faq?> GetByIdAsync(Guid id)
        {
            return await dbContext.Faqs.FindAsync(id);
        }

        public async Task<Faq> CreateAsync(Faq faq)
        {
            await dbContext.Faqs.AddAsync(faq);
            await dbContext.SaveChangesAsync();
            return faq;
        }

        public async Task<Faq?> UpdateAsync(Guid id, Faq faq)
        {
            var faqdomain = await dbContext.Faqs.FindAsync(id);

            if (faqdomain == null)
            {
                return null;
            }
            faqdomain.Answer = faq.Answer;
            faqdomain.Question = faq.Question;
            
            await dbContext.SaveChangesAsync();
            return faqdomain;
        }

        public async Task<Faq?> DeleteAsync(Guid id)
        {
            var dbfaq = await dbContext.Faqs.FirstOrDefaultAsync(x=>x.Id == id);

            if (dbfaq == null)
            {
                return null;
            }

            dbContext.Faqs.Remove(dbfaq);
            dbContext.SaveChanges();
            return dbfaq;
        }
    }
}
