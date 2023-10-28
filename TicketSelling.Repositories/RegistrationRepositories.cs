using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TicketSelling.General;
using TicketSelling.Repositories.Anchors;

namespace TicketSelling.Repositories
{
    public static class RegistrationRepositories
    {
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
