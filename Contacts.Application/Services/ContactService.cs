// Contacts.Application/Services/ContactService.cs
using Contacts.Domain.Entities;
using Contacts.Application.DTOs;
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

        public async Task AddContactAsync(ContactCreateDto contactCreateDto)
        {

            var contact = new Contact
            {
                Name = contactCreateDto.Name,
                Email = contactCreateDto.Email,
                DDD = contactCreateDto.DDD,
                Phone = contactCreateDto.Phone
            };
            await _contactRepository.AddAsync(contact);
        }

        public async Task UpdateContactAsync(ContactUpdateDto contactUpdateDto)
        {
            var contact = await _contactRepository.GetByIdAsync(contactUpdateDto.Id);
            if (contact == null) return;
            contact.Name = contactUpdateDto.Name;
            contact.Email = contactUpdateDto.Email;
            contact.DDD = contactUpdateDto.DDD;
            contact.Phone = contactUpdateDto.Phone;

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            var contactDtos = new List<ContactDto>();

            foreach (var contact in contacts)
            {
                contactDtos.Add(new ContactDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    DDD = contact.DDD,
                    Phone = contact.Phone
                });
            }

            return contactDtos;
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null) return null;

            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                DDD = contact.DDD,
                Phone = contact.Phone
            };
        }

        public async Task<IEnumerable<ContactDto>> GetContactsByDDDAsync(string ddd)
        {
            var contacts = await _contactRepository.GetByDDDAsync(ddd);
            var contactDtos = contacts.Select(contact => new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                DDD = contact.DDD,
                Phone = contact.Phone
            });

            return contactDtos;
        }

        public async Task DeleteContactAsync(ContactDeleteDto contactDeleteDto)
        {
            await _contactRepository.DeleteAsync(contactDeleteDto.Id);
        }


    }
}