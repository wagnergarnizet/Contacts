using Fiap.Team10.Contacts.Domain.Interfaces.Repositories;
using Fiap.Team10.Contacts.Infrastructure.Data;
using Fiap.Team10.Contacts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Team10.Contacts.CrossCutting.DependencyInjections
{
    public static class RepositoriesDependency
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactRepository, ContactRepository>();

            return service;
        }

        public static IServiceCollection AddDbContextDependency(this IServiceCollection service, string connectionString)
        {
            // Add services to the container.
            service.AddDbContext<ContactsDbContext>(options => options.UseMySql(connectionString,
                                                               new MySqlServerVersion(new Version(8, 0, 21)),
                                                               mySqlOptions => mySqlOptions.MigrationsAssembly("Contacts.Infrastructure")));

            return service;
        }
    }
}
