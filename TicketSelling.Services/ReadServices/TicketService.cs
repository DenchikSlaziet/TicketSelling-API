using AutoMapper;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class TicketService : ITicketService, IServiceAnchor
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
            var cinemas = await cinemaReadRepository
                .GetByIdsAsync(tickets.Select(x => x.CinemaId).Distinct(), cancellationToken);

            var clients = await clientReadRepository
                .GetByIdsAsync(tickets.Select(x => x.ClientId).Distinct(), cancellationToken);

            var films = await filmReadRepository
                .GetByIdsAsync(tickets.Select(x => x.FilmId).Distinct(), cancellationToken);

            var halls = await hallReadRepository
                .GetByIdsAsync(tickets.Select(x => x.HallId).Distinct(), cancellationToken);

            var staffs = await staffReadRepository
                .GetByIdsAsync(tickets.Select(x => x.StaffId).Distinct(), cancellationToken);

            var result = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                cinemas.TryGetValue(ticket.CinemaId, out var cinema);
                clients.TryGetValue(ticket.ClientId, out var client);
                films.TryGetValue(ticket.FilmId, out var film);
                halls.TryGetValue(ticket.HallId, out var hall);
                staffs.TryGetValue(ticket.StaffId, out var staff);

                var ticketModel = mapper.Map<TicketModel>(ticket);

                ticketModel.Hall = mapper.Map<HallModel>(hall);
                ticketModel.Film = mapper.Map<FilmModel>(film);
                ticketModel.Staff = mapper.Map<StaffModel>(staff);
                ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
                ticketModel.Client = mapper.Map<ClientModel>(client);

                result.Add(ticketModel);
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

            var cinema = await cinemaReadRepository.GetByIdAsync(item.CinemaId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(item.FilmId, cancellationToken);
            var hall = await hallReadRepository.GetByIdAsync(item.HallId, cancellationToken);
            var staff = await staffReadRepository.GetByIdAsync(item.StaffId, cancellationToken);
            var client = await clientReadRepository.GetByIdAsync(item.ClientId, cancellationToken);
            var ticketModel = mapper.Map<TicketModel>(item);

            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);
            ticketModel.Staff = mapper.Map<StaffModel>(staff);
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Client = mapper.Map<ClientModel>(client);

            return ticketModel;
        }
    }
}
