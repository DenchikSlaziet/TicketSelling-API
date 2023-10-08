using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface IFilmService
    {
        /// <summary>
        /// Получить список всех <see cref="FilmModel"/>
        /// </summary>
        Task<IEnumerable<FilmModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="FilmModel"/> по идентификатору
        /// </summary>
        Task<FilmModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
