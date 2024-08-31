using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public interface ISponsorRepository
    {
        Task<List<Sponsors>> GetAllAsync();
        Task<Sponsors?> GetByIdAsync(Guid id);
        Task<Sponsors> CreateAsync(Sponsors faq);
        Task<Sponsors?> UpdateAsync(Guid id, Sponsors faq);
        Task<Sponsors?> DeleteAsync(Guid id);
    }
}
