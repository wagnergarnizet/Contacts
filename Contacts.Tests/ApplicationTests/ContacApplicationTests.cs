using Fiap.Team10.Contacts.Application.Services;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Team10.Contacts.Tests.ApplicationTests
{
    public class ContacApplicationTests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly Mock<ILogger<ContactApplication>> _loggerMock;
        private ContactApplication _contactApplication;

        public ContacApplicationTests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _loggerMock = new Mock<ILogger<ContactApplication>>();
            _contactApplication = new ContactApplication(_contactServiceMock.Object, _loggerMock.Object);            
        }       


    }
}
