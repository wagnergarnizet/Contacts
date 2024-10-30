using Fiap.Team10.Contacts.Application.Services;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Entities;
using Fiap.Team10.Contacts.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.Team10.Contacts.Tests.ApplicationTests
{
    public class ContacApplicationTests
    {
        private Mock<IContactService> _contactServiceMock;
        private Mock<ILogger<ContactApplication>> _loggerMock;
        private ContactApplication _contactApplication;

        public ContacApplicationTests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _loggerMock = new Mock<ILogger<ContactApplication>>();
            _contactApplication = new ContactApplication(_contactServiceMock.Object, _loggerMock.Object);            
        }

        [Fact]
        public async Task AddContactAsyncRepositorySuccess()
        {
            var expectedResponse = CommonTestData.GetResponseUpsertObject(true, true);

            _contactServiceMock.Setup(service => service.InsertAsync(It.IsAny<Contact>()));

            var result = await _contactApplication.AddContactAsync(CommonTestData.GetInsertDtoObject());

            Assert.True(result.Success);
            Assert.Equal(expectedResponse.Message, result.Message);
        }

        [Fact]
        public async Task AddContactAsyncRepositoryFail()
        {
            var expectedResponse = CommonTestData.GetResponseUpsertObject(false, true);
            var contactCreateDto = CommonTestData.GetInsertDtoObject();
            _contactServiceMock.Setup(service => service.InsertAsync(It.IsAny<Contact>())).ThrowsAsync(new Exception("ERRO-SIMULADO"));

            var result = await _contactApplication.AddContactAsync(contactCreateDto);

            Assert.False(result.Success);
            Assert.Contains(expectedResponse.Message, result.Message);
        }

        [Fact]
        public async Task UpdateContactAsyncRepositorySuccess()
        {
            var expectedResponse = CommonTestData.GetResponseUpsertObject(true, false);

            var contactUpdateDto = CommonTestData.GetUpdateDtoObject();
            var existingContact = CommonTestData.GetContactObject();
            _contactServiceMock.Setup(service => service.GetByIdAsync(contactUpdateDto.Id)).ReturnsAsync(existingContact);
            _contactServiceMock.Setup(service => service.UpdateAsync(existingContact)).Returns(Task.CompletedTask);

            var result = await _contactApplication.UpdateContactAsync(contactUpdateDto);

            Assert.True(result.Success);
            Assert.Equal(expectedResponse.Message, result.Message);
        }

        [Fact]
        public async Task UpdateContactAsyncRepositoryNotFound()
        {
            var contactUpdateDto = CommonTestData.GetUpdateDtoObject();
            _contactServiceMock.Setup(service => service.GetByIdAsync(contactUpdateDto.Id)).ReturnsAsync((Contact)null);

            var result = await _contactApplication.UpdateContactAsync(contactUpdateDto);

            Assert.False(result.Success);
            Assert.Equal("Contato não encontrado", result.Message);
        }

        [Fact]
        public async Task GetAllContactsAsyncRepositorySuccess()
        {
            var contacts = CommonTestData.GetContacListObject();

            _contactServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(contacts);

            var result = await _contactApplication.GetAllContactsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _contactServiceMock.Verify(service => service.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllContactsAsyncRepositoryFail()
        {
            _contactServiceMock.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception("ERRO-SIMULADO"));

            var result = await _contactApplication.GetAllContactsAsync();

            Assert.Null(result);
            _contactServiceMock.Verify(service => service.GetAllAsync(), Times.Once);
            _loggerMock.Equals("Ocorreu um erro na consulta de todos os contatos. Erro: ERRO-SIMULADO");
        }

        [Fact]
        public async Task GetContactByIdAsyncRepositorySuccess()
        {
            var contactId = 1;
            var contact = CommonTestData.GetContactObject();
            _contactServiceMock.Setup(service => service.GetByIdAsync(contactId)).ReturnsAsync(contact);

            var result = await _contactApplication.GetContactByIdAsync(contactId);

            Assert.NotNull(result);
            Assert.Equal(contactId, result.Id);
            _contactServiceMock.Verify(service => service.GetByIdAsync(contact.Id), Times.Once);
        }

        [Fact]
        public async Task GetContactByIdAsyncRepositoryFail()
        {
            var contactId = 1;
            _contactServiceMock.Setup(service => service.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            var result = await _contactApplication.GetContactByIdAsync(contactId);

            Assert.Null(result);
            _contactServiceMock.Verify(service => service.GetByIdAsync(contactId), Times.Once);
        }

        [Fact]
        public async Task GetContactsByAreaCodeAsyncRepositorySuccess()
        {
            var areaCode = "11";
            var contactList = CommonTestData.GetContacListObject();

            _contactServiceMock.Setup(service => service.GetByAreaCodeAsync(areaCode)).ReturnsAsync(contactList);

            var result = await _contactApplication.GetContactsByAreaCodeAsync(areaCode);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _contactServiceMock.Verify(service => service.GetByAreaCodeAsync(areaCode), Times.Once);
        }

        [Fact]
        public async Task DeleteContactAsyncRepositorySuccess()
        {
            var contactDeleteDto = new ContactDeleteDto { Id = 1 };

            await _contactApplication.DeleteContactAsync(contactDeleteDto);

            _contactServiceMock.Verify(service => service.DeleteAsync(contactDeleteDto.Id), Times.Once);
        }
    }
}
