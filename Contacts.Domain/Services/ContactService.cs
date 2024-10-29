using Fiap.Team10.Contacts.Domain.Entities;
using Fiap.Team10.Contacts.Domain.Interfaces.Repositories;
using Fiap.Team10.Contacts.Domain.Interfaces.Services;

namespace Fiap.Team10.Contacts.Domain.Services
{
    public class ContactService(IContactRepository contactRepository) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task InsertAsync(Contact contact) 
            => await _contactRepository.AddAsync(contact);

        public async Task<Contact> GetByIdAsync(int id)
            => await _contactRepository.GetByIdAsync(id);

        public async Task<bool> UpdateAsync(Contact contactUpdate)
        {
            try
            {
                await _contactRepository.UpdateAsync(contactUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
            => await _contactRepository.GetAllAsync();

        public async Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode)
            => await _contactRepository.GetByAreaCodeAsync(areaCode);

        public async Task DeleteAsync(int id)
            => await _contactRepository.DeleteAsync(id);
    }
}