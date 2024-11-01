using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Presentation.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Team10.Contacts.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactApplication contactService, ILogger<ContactsController> logger) : ControllerBase
    {
        private readonly IContactApplication _contactService = contactService;
        private readonly ILogger<ContactsController> _logger = logger;

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
            var insertedObject = await _contactService.AddContactAsync(contact);

            if (insertedObject.Success)
                return Ok(insertedObject.Message);
            else
                return BadRequest(insertedObject.Message);
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
            var updatedObject = await _contactService.UpdateContactAsync(contact);

            if (updatedObject.Success)
                return Ok(updatedObject.Message);
            else 
                return BadRequest(updatedObject.Message);
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
            _logger.LogInformation($"Buscando contato de ID {id}");
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                _logger.LogWarning($"Contato de ID {id} não encontrado");
                return NotFound();
            }

            _logger.LogInformation($"Contato de ID {id} encontrado");
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
        /// <param name="areaCode"> informar o DDD do contato</param>
        /// <returns> Retorna uma lista de contatos filtrados pelo DDD no formato Json</returns>
        [HttpGet("ddd/{areaCode}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ContactDto>> GetContactsByAreaCode(string areaCode)
        {
            CustomLogger.Arquivo = true;
            _logger.LogInformation("Buscando contatos pelo DDD {DDD}", areaCode);
                        
            try
            {
                return await _contactService.GetContactsByAreaCodeAsync(areaCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ContactDto>();
            }
        }
    }
}
