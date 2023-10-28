using System.Diagnostics.CodeAnalysis;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    internal class CinemaWriteRepository : ICinemaWriteRepository, IRepositoryAnchor
    {
        private IWriter writer;

        public CinemaWriteRepository(IWriter writer)
        {
            this.writer = writer;
        }

        void ICinemaWriteRepository.AddCinema([NotNull] Cinema cinema)
        {     
            writer.Add<Cinema>(cinema);
        }
    }
}
