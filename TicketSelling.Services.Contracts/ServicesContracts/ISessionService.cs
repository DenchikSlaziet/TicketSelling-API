using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <<see cref="SessionModel"/>
    /// </summary>
    public interface ISessionService
    {
        // <summary>
        /// Получить список всех <see cref="SessionModel/>
        /// </summary>
        Task<IEnumerable<SessionModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="SessionModel"/> по идентификатору
        /// </summary>
        Task<SessionModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="SessionModel"/>
        /// </summary>
        Task<SessionModel> AddAsync(SessionRequestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="SessionModel"/>
        /// </summary>
        Task<SessionModel> EditAsync(SessionRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="SessionModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
