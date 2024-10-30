using Fiap.Team10.Contacts.Domain.DTOs.Application;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;

namespace Fiap.Team10.Contacts.Domain.Interfaces.Applications
{
    public interface IContactApplication
    {
        Task<UpsertContactResponse> AddContactAsync(ContactCreateDto contactCreateDto);

        Task<UpsertContactResponse> UpdateContactAsync(ContactUpdateDto contactUpdateDto);

        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        
        Task<ContactDto?> GetContactByIdAsync(int id);

        Task<IEnumerable<ContactDto>> GetContactsByAreaCodeAsync(string areaCode);

        Task DeleteContactAsync(ContactDeleteDto contactDeleteDto);
    }
}