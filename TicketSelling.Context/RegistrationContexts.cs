using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts;

namespace TicketSelling.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<ITicketSellingContext>(provider => provider.GetRequiredService<TicketSellingContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<TicketSellingContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<TicketSellingContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TicketSellingContext>());
        }
    }
}
