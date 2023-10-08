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
    }
}
