using Microsoft.Extensions.DependencyInjection;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Interfaces;

namespace TicketSelling.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.AddScoped<IWriter, TicketSellingContext>();
            service.AddScoped<IReader, TicketSellingContext>();
            service.AddScoped<IUnitOfWork, TicketSellingContext>();
        }
    }
}
