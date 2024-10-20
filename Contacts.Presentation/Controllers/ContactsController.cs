﻿// Contacts.Presentation/Controllers/ContactsController.cs
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
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactCreateDto contact)
        {
            _logger.LogInformation("Adicionando novo contato");
            await _contactService.AddContactAsync(contact);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(ContactUpdateDto contact)
        {
            _logger.LogInformation("Atualizando contato de ID {ID}", contact.Id);
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            _logger.LogInformation("Buscando todos os contatos");
            return await _contactService.GetAllContactsAsync();
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> DeleteContact(int id)
        {
            _logger.LogInformation("Deletando contato de ID {ID}", id);
            await _contactService.DeleteContactAsync(new ContactDeleteDto { Id = id });
            return Ok();
        }

        [HttpGet("ddd/{ddd}")]
        public async Task<IEnumerable<ContactDto>> GetContactsByDDD(string ddd)
        {
            _logger.LogInformation("Buscando contatos pelo DDD {DDD}", ddd);
            return await _contactService.GetContactsByDDDAsync(ddd);
        }
    }
}
