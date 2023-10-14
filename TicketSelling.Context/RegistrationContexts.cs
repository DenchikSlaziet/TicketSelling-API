using Microsoft.Extensions.DependencyInjection;
using TicketSelling.Context.Contracts;

namespace TicketSelling.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.AddSingleton<ITicketSellingContext, TicketSellingContext>();
        }
    }
}
