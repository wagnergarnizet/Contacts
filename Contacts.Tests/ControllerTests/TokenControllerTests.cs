using Fiap.Team10.Contacts.Domain.Entities;
using Fiap.Team10.Contacts.Domain.Interfaces.Applications;
using Fiap.Team10.Contacts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Team10.Contacts.Tests.ControllerTests
{
    public class TokenControllerTests
    {
        [Fact]
        public void GetTokenValidUserTokenSuccess()
        {
            var usuario = new User
            {
                Username = "admin",
                Password = "admin"
            };

            var tokenServiceMock = new Mock<ITokenApplication>();
            tokenServiceMock.Setup(x => x.GetToken(usuario)).Returns("valid_token");

            var controller = new TokenController(tokenServiceMock.Object);

            var result = controller.GetToken(usuario) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("valid_token", result.Value);
        }

        [Fact]
        public void GetTokenInvalidUserTokenFail()
        {
            var usuario = new User
            {
                Username = "invalid",
                Password = "invalid"
            };

            var tokenServiceMock = new Mock<ITokenApplication>();
            tokenServiceMock.Setup(x => x.GetToken(usuario)).Returns(string.Empty);

            var controller = new TokenController(tokenServiceMock.Object);

            var result = controller.GetToken(usuario) as UnauthorizedResult;

            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}