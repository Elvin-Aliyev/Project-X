using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public interface IBCategoryRepository
    {
        Task<List<BCategory>> GetAllAsync();
        Task<BCategory?> GetByIdAsync(Guid id);
        Task<BCategory> CreateAsync(BCategory faq);
        Task<BCategory?> UpdateAsync(Guid id, BCategory faq);
        Task<BCategory?> DeleteAsync(Guid id);
    }
}
