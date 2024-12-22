using Fiap.Team10.Contacts.Domain.Enumerators;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Team10.Contacts.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public static class UserList
    {
        public static IList<User>? Users { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public TypePermission TypePermission { get; set; }
    }
}