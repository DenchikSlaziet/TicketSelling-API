using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="UserModel"/>
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить список всех <see cref="UserModel"/>
        /// </summary>
        Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="UserModel"/> по идентификатору
        /// </summary>
        Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        Task<UserModel> AddAsync(UserModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        Task<UserModel> EditAsync(UserModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
