using System.Diagnostics.CodeAnalysis;

namespace Fiap.Team10.Contacts.Domain.DTOs.Application
{

    [ExcludeFromCodeCoverage]
    public class UpdateContactResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
