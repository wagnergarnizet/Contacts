using Fiap.Team10.Contacts.Domain.Entities;

namespace Fiap.Team10.Contacts.Domain.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> GetByAreaCodeAsync(string ddd);
        Task DeleteAsync(int id);
    }
}
