// Contacts.Tests/ContactServiceTests.cs
using Contacts.Application.DTOs;
using Contacts.Application.Services;
using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
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







    }
}