
using System.Collections.Generic;

namespace Contacts.Domain.Entities
{

    public static class ListaUsuario
    {
        public static IList<Usuario>? Usuarios  { get; set; }
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public TipoPermissaoSistema PermissaoSistema { get; set; }
    }
}