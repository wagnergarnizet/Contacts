
using Contacts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Domain.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> GetByDDDAsync(string ddd);
        Task DeleteAsync(int id);
    }
}
