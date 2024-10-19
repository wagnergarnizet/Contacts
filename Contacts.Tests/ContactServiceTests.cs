// Contacts.Tests/ContactServiceTests.cs
using Contacts.Application.Services;
using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Contacts.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockRepo;
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            _mockRepo = new Mock<IContactRepository>();
            _service = new ContactService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddContactAsync_ShouldAddContact()
        {
            var contact = new Contact { Name = "Wagner Garnizet", Phone = "967918795", DDD = "11", Email = "wagnergarnizet@hotmail.com" };
            await _service.AddContactAsync(contact);
            _mockRepo.Verify(r => r.AddAsync(contact), Times.Once);
        }


        [Fact]
        public async Task GetAllContactsAsync_ShouldReturnAllContacts()
        {
            var contacts = new List<Contact> { new Contact { Name = "Wagner Garnizet", Phone = "967918795", DDD = "11", Email = "wagnergarnizet@hotmail.com" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(contacts);
            var result = await _service.GetAllContactsAsync();
            Assert.Equal(contacts, result);
        }
    }
}