using Fiap.Team10.Contacts.Domain.Interfaces.Services;
using Fiap.Team10.Contacts.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Team10.Contacts.CrossCutting.DependencyInjections
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactService, ContactService>();

            return service;
        }
    }
}
