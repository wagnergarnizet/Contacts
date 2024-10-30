using Fiap.Team10.Contacts.Domain.DTOs.Application;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.Team10.Contacts.Tests.ControllerTests
{
    public class ContactsControllerTests
    {
        private Mock<IContactApplication> _contactAppServiceMock;
        private Mock<ILogger<ContactsController>> _loggerMock;
        private ContactsController _controller;

        public ContactsControllerTests()
        {
            _contactAppServiceMock = new Mock<IContactApplication>();
            _loggerMock = new Mock<ILogger<ContactsController>>();
            _controller = new ContactsController(_contactAppServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task AddContactApplicationSuccess()
        {
            var contact = CommonTestData.GetInsertDtoObject();

            var insertReturn = CommonTestData.GetResponseUpsertObject(true, true);

            _contactAppServiceMock.Setup(x => x.AddContactAsync(contact)).ReturnsAsync(insertReturn);

            var result = await _controller.AddContact(contact);

            Assert.IsType<OkObjectResult>(result);
            _contactAppServiceMock.Verify(service => service.AddContactAsync(It.IsAny<ContactCreateDto>()), Times.Once);
        }

        [Fact]
        public async Task AddContactApplicationFail()
        {
            var contact = CommonTestData.GetInsertDtoObject();

            var insertReturn = CommonTestData.GetResponseUpsertObject(false, true);

            _contactAppServiceMock.Setup(x => x.AddContactAsync(contact)).ReturnsAsync(insertReturn);

            var result = await _controller.AddContact(contact);

            Assert.IsType<BadRequestObjectResult>(result);
            _contactAppServiceMock.Verify(service => service.AddContactAsync(It.IsAny<ContactCreateDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateContactApplicationSuccess()
        {
            var contact = CommonTestData.GetUpdateDtoObject();

            var returnResponse = CommonTestData.GetResponseUpsertObject(true, false);

            _contactAppServiceMock.Setup(x => x.UpdateContactAsync(contact)).ReturnsAsync(returnResponse);

            var result = await _controller.UpdateContact(contact);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, contact.Id);
            _contactAppServiceMock.Verify(service => service.UpdateContactAsync(It.IsAny<ContactUpdateDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateContactApplicationFail()
        {
            var contact = CommonTestData.GetUpdateDtoObject();

            var returnResponse = CommonTestData.GetResponseUpsertObject(false, false);

            _contactAppServiceMock.Setup(x => x.UpdateContactAsync(contact)).ReturnsAsync(returnResponse);

            var result = await _controller.UpdateContact(contact);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(1, contact.Id);
            _contactAppServiceMock.Verify(service => service.UpdateContactAsync(It.IsAny<ContactUpdateDto>()), Times.Once);
        }

        [Fact]
        public async Task GetAllContactsApplicationSuccess()
        {
            var result = await _controller.GetAllContacts();

            Assert.NotNull(result);
            _contactAppServiceMock.Verify(service => service.GetAllContactsAsync(), Times.Once);
            _loggerMock.Equals("Buscando todos os contatos");
        }

        [Fact]
        public async Task GetContactByIdApplicationSuccess()
        {
            var contactId = 1;
            var expectedContact = new ContactDto 
                                  {
                                      Id = 1,
                                      Name = "Marcelo Cedro",
                                      AreaCode = "11",
                                      Phone = "982840611",
                                      Email = "marceloced@gmail.com"
                                  };

            _contactAppServiceMock.Setup(service => service.GetContactByIdAsync(contactId)).ReturnsAsync(expectedContact);

            var result = await _controller.GetContactById(contactId);

            var actionResult = Assert.IsType<ActionResult<ContactDto>>(result);
            var okResult = Assert.IsType<ContactDto>(actionResult.Value);
            Assert.Equal(expectedContact, okResult);
            _loggerMock.Equals($"Contato de ID {contactId} encontrado");
        }

        [Fact]
        public async Task GetContactByIdApplicationFail()
        {
            var contactId = 1;
            _contactAppServiceMock.Setup(service => service.GetContactByIdAsync(contactId));

            var result = await _controller.GetContactById(contactId);

            var actionResult = Assert.IsType<ActionResult<ContactDto>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            _loggerMock.Equals($"Contato de ID {contactId} não encontrado");
        }


        [Fact]
        public async Task DeleteContactApplicationSuccess()
        {
            var id = 1;

            var result = await _controller.DeleteContact(id);

            Assert.IsType<OkResult>(result);
            _contactAppServiceMock.Verify(service => service.DeleteContactAsync(It.IsAny<ContactDeleteDto>()), Times.Once);
        }

        [Fact]
        public async Task GetContactsByDDDApplicationSuccess()
        {
            var areaCode = "11";
            var expectedResult = new List<ContactDto>
            {
                new ()
                {
                    Id = 1,
                    Name = "Marcelo Cedro",
                    AreaCode = "11",
                    Phone = "982840611",
                    Email = "marceloced@gmail.com"
                },
                new()
                {
                    Id = 1,
                    Name = "Marcelo Affonso",
                    AreaCode = "11",
                    Phone = "982840612",
                    Email = "marceloaffonso@gmail.com"
                }
            };

            _contactAppServiceMock.Setup(service => service.GetContactsByAreaCodeAsync(areaCode)).ReturnsAsync(expectedResult);

            var resultContactsList = await _controller.GetContactsByDDD(areaCode);

            Assert.NotNull(resultContactsList);
            Assert.Equal(expectedResult.Count, resultContactsList.Count());
            _contactAppServiceMock.Verify(service => service.GetContactsByAreaCodeAsync(areaCode), Times.Once);
        }

        [Fact]
        public async Task GetContactsByDDDApplicationException()
        {
            var areaCode = "11";
            _contactAppServiceMock.Setup(service => service.GetContactsByAreaCodeAsync(areaCode)).ThrowsAsync(new Exception("Não foi possivel recuperar os contatos"));

            var result = await _controller.GetContactsByDDD(areaCode);

            Assert.Empty(result);
            _loggerMock.Equals("Não foi possivel recuperar os contatos");
            _contactAppServiceMock.Verify(service => service.GetContactsByAreaCodeAsync(areaCode), Times.Once);
        }
    }
}
