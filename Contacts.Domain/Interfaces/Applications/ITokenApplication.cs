using Fiap.Team10.Contacts.Domain.Entities;

namespace Fiap.Team10.Contacts.Domain.Interfaces.Applications
{
    public interface ITokenApplication
    {
        public string GetToken(User usuario);
    }
}
