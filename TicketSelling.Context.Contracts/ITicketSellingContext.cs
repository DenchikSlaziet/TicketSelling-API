using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts
{
    public interface ITicketSellingContext
    {
        /// <summary>Список <inheritdoc cref="Session"/></summary>
        DbSet<Session> Sessions { get; }

        /// <summary>Список <inheritdoc cref="User"/></summary>
        DbSet<User> Users { get; }

        /// <summary>Список <inheritdoc cref="Film"/></summary>
        DbSet<Film> Films { get; }

        /// <summary>Список <inheritdoc cref="Hall"/></summary>
        DbSet<Hall> Halls { get; }

        /// <summary>Список <inheritdoc cref="Staff"/></summary>
        DbSet<Staff> Staffs { get; }

        /// <summary>Список <inheritdoc cref="Ticket"/></summary>
        DbSet<Ticket> Tickets { get; }
    }
}
