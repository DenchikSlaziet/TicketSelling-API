using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts
{
    public interface ITicketSellingContext
    {
        /// <summary>Список <inheritdoc cref="Cinema"/></summary>
        DbSet<Cinema> Cinemas { get; }

        /// <summary>Список <inheritdoc cref="Client"/></summary>
        DbSet<Client> Clients { get; }

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
