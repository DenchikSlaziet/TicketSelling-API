using Microsoft.EntityFrameworkCore;
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
        DbSet<Cinema> Cinemas { get; }

        /// <summary>
        /// Билеты
        /// </summary>
        DbSet<Ticket> Tickets { get; }

        /// <summary>
        /// Клиенты
        /// </summary>
        DbSet<Client> Clients { get; }

        /// <summary>
        /// Фильмы
        /// </summary>
        DbSet<Film> Films { get; }

        /// <summary>
        /// Залы
        /// </summary>
        DbSet<Hall> Halls { get; }

        /// <summary>
        /// Персонал
        /// </summary>
        DbSet<Staff> Staffs { get; }

    }
}
