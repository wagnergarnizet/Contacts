using Fiap.Team10.Contacts.Domain.DTOs.Application;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Entities;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Fiap.Team10.Contacts.Application.Services
{
    public class ContactApplication(IContactService contactService, ILogger<ContactApplication> logger) : IContactApplication
    {
        private readonly IContactService _contactService = contactService;

        private readonly ILogger<ContactApplication> _logger;

        public async Task AddContactAsync(ContactCreateDto contactCreateDto)
        {
            var contact = new Contact
            {
                Name = contactCreateDto.Name,
                Email = contactCreateDto.Email,
                AreaCode = contactCreateDto.AreaCode,
                Phone = contactCreateDto.Phone
            };
            await _contactService.InsertAsync(contact);
        }

        public async Task<UpdateContactResponse> UpdateContactAsync(ContactUpdateDto contactUpdateDto)
        {
            var updateResult = new UpdateContactResponse();
            var contactFound = await _contactService.GetByIdAsync(contactUpdateDto.Id);
            if (contactFound == null)
            {
                updateResult.Success = false;
                updateResult.Message = "Contato não encontrado";

                _logger.LogInformation(updateResult.Message);
            }
            else
            {
                contactFound.Name = contactUpdateDto.Name;
                contactFound.Email = contactUpdateDto.Email;
                contactFound.AreaCode = contactUpdateDto.AreaCode;
                contactFound.Phone = contactUpdateDto.Phone;

                var updatedContact = await _contactService.UpdateAsync(contactFound);

                if (!updatedContact) 
                {
                    updateResult.Success = false;
                    updateResult.Message = "Ocorreu um problema ao tentar atualizar o registro.";
                    _logger.LogError(updateResult.Message);
                }
                else
                {
                    updateResult.Success = true;
                    updateResult.Message = "Contato alterado com sucesso.";
                }
            }

            return updateResult;
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contacts = await _contactService.GetAllAsync();
            var contactDtos = new List<ContactDto>();

            foreach (var contact in contacts)            
                contactDtos.Add(new ContactDto
                                {
                                    Id = contact.Id,
                                    Name = contact.Name,
                                    Email = contact.Email,
                                    AreaCode = contact.AreaCode,
                                    Phone = contact.Phone
                                });            

            return contactDtos;
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) 
                return null;

            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                AreaCode = contact.AreaCode,
                Phone = contact.Phone
            };
        }

        public async Task<IEnumerable<ContactDto>> GetContactsByAreaCodeAsync(string areaCode)
        {
            var contacts = await _contactService.GetByAreaCodeAsync(areaCode);
            var contactDtos = contacts.Select(contact => new ContactDto
                                              {
                                                  Id = contact.Id,
                                                  Name = contact.Name,
                                                  Email = contact.Email,
                                                  AreaCode = contact.AreaCode,
                                                  Phone = contact.Phone
                                              });

            return contactDtos;
        }

        public async Task DeleteContactAsync(ContactDeleteDto contactDeleteDto)
            => await _contactService.DeleteAsync(contactDeleteDto.Id);
    }
}