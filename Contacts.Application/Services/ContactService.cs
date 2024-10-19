// Contacts.Application/Services/ContactService.cs
using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Contacts.Application.Services
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task AddContactAsync(Contact contact)
        {
            ValidateContact(contact);
            await _contactRepository.AddAsync(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            ValidateContact(contact);
            await _contactRepository.UpdateAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {

            return await _contactRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByDDDAsync(string ddd)
        {
            return await _contactRepository.GetByDDDAsync(ddd);
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }
        private void ValidateContact(Contact contact)
        {
            var validationContext = new ValidationContext(contact);
            Validator.ValidateObject(contact, validationContext, validateAllProperties: true);
        }
    }
}