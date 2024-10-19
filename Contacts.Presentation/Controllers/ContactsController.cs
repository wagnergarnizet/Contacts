// Contacts.Presentation/Controllers/ContactsController.cs
using Contacts.Application.Services;
using Contacts.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            await _contactService.AddContactAsync(contact);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(Contact contact)
        {
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactService.GetAllContactsAsync();
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok();
        }

        [HttpGet("ddd/{ddd}")]
        public async Task<IEnumerable<Contact>> GetContactsByDDD(string ddd)
        {
            return await _contactService.GetContactsByDDDAsync(ddd);
        }
    }
}
