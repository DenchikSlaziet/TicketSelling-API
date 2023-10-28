using System.Diagnostics.CodeAnalysis;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.WriteRepositoriesContracts
{
    public interface ICinemaWriteRepository
    {
        void AddCinema([NotNull] Cinema entity);
    }
}
