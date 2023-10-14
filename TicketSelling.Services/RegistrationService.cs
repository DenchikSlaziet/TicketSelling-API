using Microsoft.Extensions.DependencyInjection;
using TicketSelling.General;
using TicketSelling.Services.Anchors;

namespace TicketSelling.Services
{
    public static class RegistrationService
    {
        public static void RegistrationServices(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }       
    }
}
