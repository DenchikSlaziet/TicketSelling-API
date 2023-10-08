using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface IHallService
    {
        /// <summary>
        /// Получить список всех <see cref="HallModel"/>
        /// </summary>
        Task<IEnumerable<HallModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="HallModel"/> по идентификатору
        /// </summary>
        Task<HallModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
