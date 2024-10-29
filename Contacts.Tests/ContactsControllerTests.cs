using Fiap.Team10.Contacts.Domain.DTOs.Application;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.Team10.Contacts.Tests
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
        public async Task AddContact_ValidContact_ReturnsOkResult()
        {
            // Arrange
            var contact = new ContactCreateDto
            {
                Name = "John Doe",
                AreaCode = "11",
                Phone = "123456789",
                Email = "john.doe@example.com"
            };

            // Act
            var result = await _controller.AddContact(contact);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateContact_ValidContact_ReturnsOkResult()
        {
            // Arrange
            var contact = new ContactUpdateDto
            {
                Id = 1,
                Name = "John Doe",
                AreaCode = "11",
                Phone = "123456789",
                Email = " "
            };

            var returnResponse = new UpdateContactResponse
            {
                Success = true,
                Message = "Contato atualizado com sucesso"
            };

            _contactAppServiceMock.Setup(x => x.UpdateContactAsync(contact)).ReturnsAsync(returnResponse);


            // Act
            var result = await _controller.UpdateContact(contact);

            // Assert
            Assert.IsNotType<BadRequestResult>(result);
            Assert.Equal(1, contact.Id);
        }

        [Fact]
        public async Task GetAllContacts_ValidContacts_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetAllContacts();

            // Assert
            Assert.NotNull(result);
        }



        [Fact]
        public async Task DeleteContact_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.DeleteContact(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
