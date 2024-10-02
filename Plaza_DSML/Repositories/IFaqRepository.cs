using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Data;
using Plaza_DSML.Models;

namespace Plaza_DSML.Repositories
{
    public interface IFaqRepository
    {
        Task<List<Faq>> GetAllAsync();
        Task<Faq?> GetByIdAsync(Guid id);
        Task<Faq> CreateAsync(Faq faq);
        Task<Faq?> UpdateAsync(Guid id, Faq faq);
        Task<Faq?> DeleteAsync(Guid id);
    }
    public class FaqRepository(AppDbContext dbContext) : IFaqRepository
    {
        public async Task<Faq> CreateAsync(Faq faq)
        {
            await dbContext.Faqs.AddAsync(faq);
            await dbContext.SaveChangesAsync();
            return faq;
        }

        public async Task<Faq?> DeleteAsync(Guid id)
        {
            var dbfaq = await dbContext.Faqs.FirstOrDefaultAsync(x => x.Id == id);

            if (dbfaq == null)
            {
                return null;
            }

            dbContext.Faqs.Remove(dbfaq);
            dbContext.SaveChanges();
            return dbfaq;
        }

        public async Task<List<Faq>> GetAllAsync()
        {
            return await dbContext.Faqs.ToListAsync();
        }

        public async Task<Faq?> GetByIdAsync(Guid id)
        {
            return await dbContext.Faqs.FindAsync(id);
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
    }
}
