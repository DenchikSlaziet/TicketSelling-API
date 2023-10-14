using Microsoft.Extensions.DependencyInjection;
using TicketSelling.General;
using TicketSelling.Services.Anchors;

namespace TicketSelling.Services
{
    public static class RegistrationServices
    {
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }       
    }
}
