using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface IStaffService
    {
        /// <summary>
        /// Получить список всех <see cref="StaffModel"/>
        /// </summary>
        Task<IEnumerable<StaffModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="StaffModel"/> по идентификатору
        /// </summary>
        Task<StaffModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового работника
        /// </summary>
        Task<StaffModel> AddAsync(string firstName, string lastName, string patronymic, short age, int post, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего работника
        /// </summary>
        Task<StaffModel> EditAsync(StaffModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего работника
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
