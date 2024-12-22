using Fiap.Team10.Contacts.CrossCutting.DependencyInjections;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Team10.Contacts.CrossCutting.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoriesDependency();
            services.AddDbContextDependency(connectionString);
            services.AddServicesDependency();
            services.AddApplicationDependency();
            services.AddAuthenticationDependency();
        }
    }
}
