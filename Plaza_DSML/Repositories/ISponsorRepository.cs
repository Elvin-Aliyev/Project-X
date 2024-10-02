using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Data;
using Plaza_DSML.Models;
using System.Reflection.Metadata;

namespace Plaza_DSML.Repositories
{
    public interface ISponsorRepository
    {
        Task<List<Sponsor>> GetAllAsync();
        Task<Sponsor?> GetByIdAsync(Guid id);
        Task<Sponsor> CreateAsync(Sponsor sponsor);
        Task<Sponsor?> UpdateAsync(/*Guid id,*/ Sponsor sponsor);
        Task<Sponsor?> DeleteAsync(/*Guid id*/  Sponsor sponsor);
    }
    public class SponsorRepository(AppDbContext dbContext) : ISponsorRepository
    {
        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            await dbContext.Sponsors.AddAsync(sponsor);
            await dbContext.SaveChangesAsync();
            return sponsor;
        }

        public async Task<Sponsor?> DeleteAsync(Sponsor sponsor)
        {
            dbContext.Sponsors.Remove(sponsor);
            dbContext.SaveChanges();
            return sponsor;
        }

        public async Task<List<Sponsor>> GetAllAsync()
        {
            return await dbContext.Sponsors.ToListAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(Guid id)
        {
            return await dbContext.Sponsors.FindAsync(id);
        }

        public async Task<Sponsor?> UpdateAsync(Sponsor sponsor)
        {
            dbContext.Sponsors.Update(sponsor);
            await dbContext.SaveChangesAsync();
            return sponsor;
        }
    }
}
