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


        /// <summary>
        /// Metodo para adicionar um novo contato de forma assíncrona.
        /// </summary>
        /// <param name="contact">Json com os dados do contato: Nome, DDD, Telefone e Email</param>
        /// <returns>Adiciona um novo contato no banco de dados</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddContact(ContactCreateDto contact)
        {
            _logger.LogInformation("Adicionando novo contato");
            await _contactService.AddContactAsync(contact);
            return Ok();
        }


        /// <summary>
        /// Método para atualizar um contato de forma assíncrona.
        /// </summary>
        /// <param name="contact">Objeto com os dados do contato a ser atualizado
        /// em formato Json com os dados do contato:ID, Nome, DDD, Telefone e Email</param>
        /// <returns>Não retorna nenhum valor, atualiza os dados no banco</returns>
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateContact(ContactUpdateDto contact)
        {
            _logger.LogInformation("Atualizando contato de ID {ID}", contact.Id);
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }


        /// <summary>
        /// Método para buscar todos os contatos de forma assíncrona.
        /// </summary>
        /// <returns> Retorna uma lista de contatos no formato Json</returns>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            _logger.LogInformation("Buscando todos os contatos");
            return await _contactService.GetAllContactsAsync();
        }



        /// <summary>
        /// Método para buscar um contato pelo ID de forma assíncrona.
        /// </summary>
        /// <param name="id"> informar o ID do contato</param>
        /// <returns>Retorna um contato filtrado pelo ID no formato Json</returns>
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


        /// <summary>
        /// Método para deletar um contato de forma assíncrona.
        /// </summary>
        /// <param name="id">Objeto com o ID do contato a ser deletado</param>
        /// <returns>Não retorna nenhum valor, deleta o contato do banco de dados</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            _logger.LogInformation("Deletando contato de ID {ID}", id);
            await _contactService.DeleteContactAsync(new ContactDeleteDto { Id = id });
            return Ok();
        }


        /// <summary>
        /// Método para buscar contatos por DDD de forma assíncrona.
        /// </summary>
        /// <param name="ddd"> informar o DDD do contato</param>
        /// <returns> Retorna uma lista de contatos filtrados pelo DDD no formato Json</returns>
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
