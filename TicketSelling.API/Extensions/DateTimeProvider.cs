using TicketSelling.Common.Entity;

namespace TicketSelling.API.Extensions
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
