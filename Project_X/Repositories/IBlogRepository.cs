using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(Guid id);
        Task<Blog> CreateAsync(Blog faq);
        Task<Blog?> UpdateAsync(Guid id, Blog faq);
        Task<Blog?> DeleteAsync(Guid id);
    }
}
