using Microsoft.EntityFrameworkCore;
using Plaza_DSML.Data;
using Plaza_DSML.Models;

namespace Plaza_DSML.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<Contact?> DeleteAsync(Guid id);
    }
    public class ContactRepository(AppDbContext dbContext) : IContactRepository
    {
        public async Task<Contact> CreateAsync(Contact contact)
        {
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> DeleteAsync(Guid id)
        {
            var dbcontact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (dbcontact == null)
            {
                return null;
            }

            dbContext.Contacts.Remove(dbcontact);
            dbContext.SaveChanges();
            return dbcontact;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await dbContext.Contacts.FindAsync(id);
        }

        public async Task<Contact?> UpdateAsync(Guid id, Contact contact)
        {
            var contactdomain = await dbContext.Contacts.FindAsync(id);

            if (contactdomain == null)
            {
                return null;
            }
            contactdomain.FullName = contact.FullName;
            contactdomain.Gmail = contact.Gmail;
            contactdomain.Phone = contact.Phone;
            contactdomain.Comment = contact.Comment;

            await dbContext.SaveChangesAsync();
            return contactdomain;
        }
    }
}
