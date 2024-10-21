using Contacts.Application.DTOs;
using Contacts.Application.Services;
using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Contacts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Contacts.Tests
{
    public class ContactsControllerTests
    {
        private Mock<ContactService> _contactServiceMock;
        private Mock<ILogger<ContactsController>> _loggerMock;
        private ContactsController _controller;

        public ContactsControllerTests()
        {
            var contactRepositoryMock = new Mock<IContactRepository>();
            _contactServiceMock = new Mock<ContactService>(contactRepositoryMock.Object);
            _loggerMock = new Mock<ILogger<ContactsController>>();
            _controller = new ContactsController(_contactServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task AddContact_ValidContact_ReturnsOkResult()
        {
            // Arrange
            var contact = new ContactCreateDto
            {
                Name = "John Doe",
                DDD = "11",
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
                DDD = "11",
                Phone = "123456789",
                Email = " "
            };

            // Act
            var result = await _controller.UpdateContact(contact);

            // Assert
            Assert.IsType<OkResult>(result);
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
