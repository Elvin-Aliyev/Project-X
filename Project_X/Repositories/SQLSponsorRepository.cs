using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public class SQLSponsorRepository:ISponsorRepository
    {

        public DSEMLDbContext dbContext { get; set; }
        public SQLSponsorRepository(DSEMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Sponsors>> GetAllAsync()
        {
            return await dbContext.Sponsors.ToListAsync();
        }

        public async Task<Sponsors?> GetByIdAsync(Guid id)
        {
            return await dbContext.Sponsors.FindAsync(id);
        }

        public async Task<Sponsors> CreateAsync(Sponsors sp)
        {
            await dbContext.Sponsors.AddAsync(sp);
            await dbContext.SaveChangesAsync();
            return sp;
        }

        public async Task<Sponsors?> UpdateAsync(Guid id, Sponsors sp)
        {
            var spdomain = await dbContext.Sponsors.FindAsync(id);

            if (spdomain == null)
            {
                return null;
            }
            spdomain.Name = sp.Name;
            spdomain.ImageUrl = sp.ImageUrl;

            await dbContext.SaveChangesAsync();
            return spdomain;
        }

        public async Task<Sponsors?> DeleteAsync(Guid id)
        {
            var dbsp = await dbContext.Sponsors.FirstOrDefaultAsync(x => x.Id == id);

            if (dbsp == null)
            {
                return null;
            }

            dbContext.Sponsors.Remove(dbsp);
            dbContext.SaveChanges();
            return dbsp;
        }
    }
}
