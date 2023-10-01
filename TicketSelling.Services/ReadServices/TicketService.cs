using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;

        public TicketService(ITicketReadRepository ticketReadRepository, ICinemaReadRepository cinemaReadRepository,
            IClientReadRepository clientReadRepository, IFilmReadRepository filmReadRepository,
            IHallReadRepository hallReadRepository, IStaffReadRepository staffReadRepository,
            IMapper mapper)
        {
            this.ticketReadRepository = ticketReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.cinemaReadRepository = cinemaReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TicketModel>> ITicketService.GetAllAsync(CancellationToken cancellationToken)
        {
            var tickets = await ticketReadRepository.GetAllAsync(cancellationToken);
            var cinemas = await cinemaReadRepository.
                GetByIdsAsync(tickets.Select(x => x.CinemaId).Distinct(), cancellationToken);

            var clients = await clientReadRepository.
                GetByIdsAsync(tickets.Select(x => x.ClientId).Distinct(), cancellationToken);

            var films = await filmReadRepository.
                GetByIdsAsync(tickets.Select(x => x.FilmId).Distinct(), cancellationToken);

            var halls = await hallReadRepository.
                GetByIdsAsync(tickets.Select(x => x.HallId).Distinct(), cancellationToken);

            var staffs = await staffReadRepository.
                GetByIdsAsync(tickets.Select(x => x.StaffId).Distinct(), cancellationToken);

            var result = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                var cinema = tickets.FirstOrDefault(x => x.Id == ticket.CinemaId);
                var client = tickets.FirstOrDefault(x => x.Id == ticket.ClientId);
                var film = tickets.FirstOrDefault(x => x.Id == ticket.FilmId);
                var hall = tickets.FirstOrDefault(x => x.Id == ticket.HallId);
                var staff = tickets.FirstOrDefault(x => x.Id == ticket.StaffId);

                result.Add(
                    new TicketModel
                    {
                        Id = ticket.Id,
                        Cinema = cinema == null ? null : mapper.Map<CinemaModel>(cinema),
                        Hall = hall == null ? null : mapper.Map<HallModel>(hall),
                        Film = film == null ? null : mapper.Map<FilmModel>(film),
                        Client = client == null ? null : mapper.Map<ClientModel>(client),
                        Staff = staff == null ? null : mapper.Map<StaffModel>(staff),
                        Date = ticket.Date,
                        Place = ticket.Place,
                        Price = ticket.Price,
                        Row = ticket.Row
                    }
                    );
            }

            return result;
        }

        async Task<TicketModel?> ITicketService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }

            var cinema = cinemaReadRepository.GetByIdAsync(item.CinemaId, cancellationToken);
            var film = filmReadRepository.GetByIdAsync(item.FilmId, cancellationToken);
            var hall = hallReadRepository.GetByIdAsync(item.HallId, cancellationToken);
            var staff = staffReadRepository.GetByIdAsync(item.StaffId, cancellationToken);
            var client = clientReadRepository.GetByIdAsync(item.ClientId, cancellationToken);


            return new TicketModel
            {
                Id = item.Id,
                Cinema = cinema == null ? null : mapper.Map<CinemaModel>(cinema),
                Hall = hall == null ? null : mapper.Map<HallModel>(hall),
                Film = film == null ? null : mapper.Map<FilmModel>(film),
                Client = client == null ? null : mapper.Map<ClientModel>(client),
                Staff = staff == null ? null : mapper.Map<StaffModel>(staff),
                Date = item.Date,
                Place = item.Place,
                Price = item.Price,
                Row = item.Row
            };
        }
    }
}
