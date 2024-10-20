using Contacts.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Contacts.Presentation.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ListaUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ListaUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            ListaUsuario.Usuarios = new List<Usuario>                {
            new Usuario { Id = 1, Username = "admin", Password = "admin", PermissaoSistema = TipoPermissaoSistema.Admin },
            new Usuario { Id = 2, Username = "user", Password = "user", PermissaoSistema = TipoPermissaoSistema.User },
            new Usuario { Id = 3, Username = "guest", Password = "guest", PermissaoSistema = TipoPermissaoSistema.Guest }
            };

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ListaUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseListaUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ListaUserMiddleware>();
        }
    }
}
