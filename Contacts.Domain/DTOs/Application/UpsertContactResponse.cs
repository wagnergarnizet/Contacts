using System.Diagnostics.CodeAnalysis;

namespace Fiap.Team10.Contacts.Domain.DTOs.Application
{

    [ExcludeFromCodeCoverage]
    public class UpsertContactResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
