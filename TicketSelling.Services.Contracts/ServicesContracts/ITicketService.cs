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

        /// <summary>
        /// Добавляет новый билет
        /// </summary>
        Task<TicketModel> AddAsync(Guid hallId, Guid filmId, Guid cinemaId, Guid clientId,
            Guid? staffId, short row, short place, decimal price, DateTimeOffset date, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий билет
        /// </summary>
        Task<TicketModel> EditAsync(TicketModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий билет
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
