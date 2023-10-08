using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface ITicketService
    {
        /// <summary>
        /// Получить список всех <see cref="TicketModel"/>
        /// </summary>
        Task<IEnumerable<TicketModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TicketModel"/> по идентификатору
        /// </summary>
        Task<TicketModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
