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

        private readonly ILogger<ContactApplication> _logger = logger;

        public async Task<UpsertContactResponse> AddContactAsync(ContactCreateDto contactCreateDto)
        {
            var insertResult = new UpsertContactResponse();
            try
            {
                var contact = new Contact
                {
                    Name = contactCreateDto.Name,
                    Email = contactCreateDto.Email,
                    AreaCode = contactCreateDto.AreaCode,
                    Phone = contactCreateDto.Phone
                };

                await _contactService.InsertAsync(contact);

                insertResult.Success = true;
                insertResult.Message = "Contato inserido com sucesso.";
            }
            catch (Exception e)
            {
                insertResult.Success = false;
                insertResult.Message = $"Ocorreu um problema ao tentar inserir o registro. Erro: {e.Message}";
                _logger.LogError(insertResult.Message);
            }

            return insertResult;
        }

        public async Task<UpsertContactResponse> UpdateContactAsync(ContactUpdateDto contactUpdateDto)
        {
            var updateResult = new UpsertContactResponse();
            try
            {

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

                    await _contactService.UpdateAsync(contactFound);

                    updateResult.Success = true;
                    updateResult.Message = "Contato alterado com sucesso.";
                }
            }
            catch (Exception e)
            {
                updateResult.Success = false;
                updateResult.Message = $"Ocorreu um problema ao tentar atualizar o registro. Erro: {e.Message}";
                _logger.LogError(updateResult.Message);
            }

            return updateResult;
        }


        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu um erro na consulta de todos os contatos. Erro: {e.Message}");
                return null;
            }
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