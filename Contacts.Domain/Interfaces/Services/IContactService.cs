using Fiap.Team10.Contacts.Domain.Entities;

namespace Fiap.Team10.Contacts.Domain.Interfaces.Services
{
    public interface IContactService
    {
        Task InsertAsync(Contact contact);

        Task<Contact> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Contact contactUpdate);

        Task<IEnumerable<Contact>> GetAllAsync();

        Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode);

        Task DeleteAsync(int id);
    }
}