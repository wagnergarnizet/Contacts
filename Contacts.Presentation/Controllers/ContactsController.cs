// Contacts.Presentation/Controllers/ContactsController.cs
using Contacts.Application.DTOs;
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
        public async Task<IActionResult> AddContact(ContactCreateDto contact)
        {
            await _contactService.AddContactAsync(contact);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(ContactUpdateDto contact)
        {
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            return await _contactService.GetAllContactsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContactById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteContactAsync(new ContactDeleteDto { Id = id });
            return Ok();
        }

        [HttpGet("ddd/{ddd}")]
        public async Task<IEnumerable<ContactDto>> GetContactsByDDD(string ddd)
        {
            return await _contactService.GetContactsByDDDAsync(ddd);
        }
    }
}
