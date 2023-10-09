using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ITicketSellingContext
    {
        /// <summary>
        /// Кинотеатры
        /// </summary>
        IEnumerable<Cinema> Cinemas { get; }

        /// <summary>
        /// Билеты
        /// </summary>
        IEnumerable<Ticket> Tickets { get; }

        /// <summary>
        /// Клиенты
        /// </summary>
        IEnumerable<Client> Clients { get; }

        /// <summary>
        /// Фильмы
        /// </summary>
        IEnumerable<Film> Films { get; }

        /// <summary>
        /// Залы
        /// </summary>
        IEnumerable <Hall> Halls { get; }

        /// <summary>
        /// Персонал
        /// </summary>
        IEnumerable<Staff> Staffs { get; }

    }
}
