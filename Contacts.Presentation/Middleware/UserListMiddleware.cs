using Fiap.Team10.Contacts.Domain.Entities;

namespace Fiap.Team10.Contacts.Presentation.Middleware
{
    public class UserListMiddleware
    {
        private readonly RequestDelegate _next;

        public UserListMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            UserList.Users = new List<User>{
                                               new User { Id = 1, Username = "admin", Password = "admin", TypePermission = TypePermission.Admin },
                                               new User { Id = 2, Username = "user", Password = "user", TypePermission = TypePermission.User },
                                               new User { Id = 3, Username = "guest", Password = "guest", TypePermission = TypePermission.Guest }
                                           };

            return _next(httpContext);
        }
    }

    public static class UserListMiddlewareExtensions
    {
        public static IApplicationBuilder UseListaUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserListMiddleware>();
        }
    }
}
