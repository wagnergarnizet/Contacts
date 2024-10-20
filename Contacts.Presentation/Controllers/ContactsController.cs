// Contacts.Presentation/Controllers/ContactsController.cs
using Contacts.Application.DTOs;
using Contacts.Application.Services;
using Contacts.Domain.Entities;
using Contacts.Presentation.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddContact(ContactCreateDto contact)
        {
            _logger.LogInformation("Adicionando novo contato");
            await _contactService.AddContactAsync(contact);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateContact(ContactUpdateDto contact)
        {
            _logger.LogInformation("Atualizando contato de ID {ID}", contact.Id);
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            _logger.LogInformation("Buscando todos os contatos");
            return await _contactService.GetAllContactsAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ContactDto>> GetContactById(int id)
        {
            _logger.LogInformation("Buscando contato de ID {ID}", id);
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                _logger.LogWarning("Contato de ID {ID} não encontrado", id);
                return NotFound();
            }
            _logger.LogInformation("Contato de ID {ID} encontrado", id);
            return contact;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            _logger.LogInformation("Deletando contato de ID {ID}", id);
            await _contactService.DeleteContactAsync(new ContactDeleteDto { Id = id });
            return Ok();
        }

        [HttpGet("ddd/{ddd}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ContactDto>> GetContactsByDDD(string ddd)
        {
            CustomLogger.Arquivo = true;
            _logger.LogInformation("Buscando contatos pelo DDD {DDD}", ddd);

            
            try
            {
                return await _contactService.GetContactsByDDDAsync(ddd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ContactDto>();
            }

        }
    }
}
