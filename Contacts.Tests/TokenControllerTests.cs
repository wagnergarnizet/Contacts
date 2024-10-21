using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Contacts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Contacts.Tests
{
    public class TokenControllerTests
    {
        [Fact]
        public void GetToken_ValidUser_ReturnsOkResultWithToken()
        {
            // Arrange
            var usuario = new Usuario
            {
                Username = "admin",
                Password = "admin"
            };

            var tokenServiceMock = new Mock<iTokenService>();
            tokenServiceMock.Setup(x => x.GetToken(usuario)).Returns("valid_token");

            var controller = new TokenController(tokenServiceMock.Object);

            // Act
            var result = controller.GetToken(usuario) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("valid_token", result.Value);
        }

        [Fact]
        public void GetToken_InvalidUser_ReturnsUnauthorizedResult()
        {
            // Arrange
            var usuario = new Usuario
            {
                Username = "invalid",
                Password = "invalid"
            };

            var tokenServiceMock = new Mock<iTokenService>();
            tokenServiceMock.Setup(x => x.GetToken(usuario)).Returns(string.Empty);

            var controller = new TokenController(tokenServiceMock.Object);

            // Act
            var result = controller.GetToken(usuario) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}