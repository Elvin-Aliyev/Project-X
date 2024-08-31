using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public interface IFaqRepository
    {
        Task<List<Faq>> GetAllAsync();
        Task<Faq?> GetByIdAsync(Guid id);
        Task<Faq> CreateAsync(Faq faq);
        Task<Faq?> UpdateAsync(Guid id,Faq faq);
        Task<Faq?> DeleteAsync(Guid id);
    }
}
