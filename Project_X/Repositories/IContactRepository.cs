using Project_X.Models.Domain;

namespace Project_X.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contract);
        Task<Contact?> UpdateAsync(Guid id, Contact contract);
        Task<Contact?> DeleteAsync(Guid id);
    }
}
