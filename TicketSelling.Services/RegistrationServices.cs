using Microsoft.Extensions.DependencyInjection;
using TicketSelling.General;
using TicketSelling.Services.Anchors;

namespace TicketSelling.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RegistrationServices
    {
        /// <summary>
        /// Регистрация всех сервисов
        /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }       
    }
}
