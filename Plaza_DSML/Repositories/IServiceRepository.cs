using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Data;
using Plaza_DSML.Models;
using System.Reflection.Metadata;

namespace Plaza_DSML.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(Guid id);
        Task<Service> CreateAsync(Service service);
        Task<Service?> UpdateAsync(/*Guid id,*/ Service service);
        Task<Service?> DeleteAsync(/*Guid id*/  Service service);
    }

    public class ServiceRepository(AppDbContext dbContext) : IServiceRepository
    {
        public async Task<Service> CreateAsync(Service service)
        {
            await dbContext.Services.AddAsync(service);
            await dbContext.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> DeleteAsync(Service service)
        {
            dbContext.Services.Remove(service);
            dbContext.SaveChanges();
            return service;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await dbContext.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await dbContext.Services.FindAsync(id);
        }

        public async Task<Service?> UpdateAsync(Service service)
        {
            dbContext.Services.Update(service);
            await dbContext.SaveChangesAsync();
            return service;
        }
    }
}
