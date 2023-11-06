using AutoMapper;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class TicketService : ITicketService, IServiceAnchor
    {
        private readonly ITicketWriteRepository ticketWriteRepository;
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TicketService(ITicketWriteRepository ticketWriteRepository, ITicketReadRepository ticketReadRepository, ICinemaReadRepository cinemaReadRepository,
            IClientReadRepository clientReadRepository, IFilmReadRepository filmReadRepository,
            IHallReadRepository hallReadRepository, IStaffReadRepository staffReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.ticketWriteRepository = ticketWriteRepository;
            this.ticketReadRepository = ticketReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.cinemaReadRepository = cinemaReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<TicketModel> ITicketService.AddAsync(Guid hallId, Guid filmId, Guid cinemaId, Guid clientId, 
            Guid? staffId, short row, short place, decimal price, DateTimeOffset date, CancellationToken cancellationToken)
        {
            var item = new Ticket
            {
                HallId = hallId,
                FilmId = filmId,
                CinemaId = cinemaId,
                ClientId = clientId,
                StaffId = staffId,
                Row = row,
                Place = place,
                Price = price,
                Date = date
            };

            ticketWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var ticketModel = mapper.Map<TicketModel>(item);

            var cinema = await cinemaReadRepository.GetByIdAsync(item.CinemaId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(item.FilmId, cancellationToken);
            var hall = await hallReadRepository.GetByIdAsync(item.HallId, cancellationToken);
            var client = await clientReadRepository.GetByIdAsync(item.ClientId, cancellationToken);

            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);           
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Client = mapper.Map<ClientModel>(client);
            ticketModel.Staff = item.StaffId.HasValue ? 
                mapper.Map<StaffModel>(await staffReadRepository.GetByIdAsync(item.StaffId.Value, cancellationToken))
                : null;

            return ticketModel;
        }

        async Task ITicketService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTicket = await ticketReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetTicket == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(id);
            }

            if (targetTicket.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Билет с идентификатором {id} уже удален");
            }

            ticketWriteRepository.Delete(targetTicket);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TicketModel> ITicketService.EditAsync(TicketModel source, CancellationToken cancellationToken)
        {
            var targetTicket = await ticketReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetTicket == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(source.Id);
            }

            targetTicket.CinemaId = source.Cinema!.Id;
            targetTicket.FilmId = source.Film!.Id;
            targetTicket.HallId = source.Hall!.Id;
            targetTicket.ClientId = source.Client!.Id;
            targetTicket.StaffId = source.Staff != null ? source.Staff.Id : Guid.Empty;
            targetTicket.Date = source.Date;
            targetTicket.Place = source.Place;
            targetTicket.Price = source.Price;
            targetTicket.Row = source.Row;

            ticketWriteRepository.Update(targetTicket);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            var ticketModel = mapper.Map<TicketModel>(targetTicket);

            var cinema = await cinemaReadRepository.GetByIdAsync(targetTicket.CinemaId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(targetTicket.FilmId, cancellationToken);
            var hall = await hallReadRepository.GetByIdAsync(targetTicket.HallId, cancellationToken);
            var client = await clientReadRepository.GetByIdAsync(targetTicket.ClientId, cancellationToken);

            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Client = mapper.Map<ClientModel>(client);
            ticketModel.Staff = targetTicket.StaffId.HasValue ?
                mapper.Map<StaffModel>(await staffReadRepository.GetByIdAsync(targetTicket.StaffId.Value, cancellationToken))
                : null;

            return ticketModel;
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
                .GetByIdsAsync(tickets.Where(x => x.StaffId.HasValue).Select(x => x.StaffId!.Value).Distinct(), cancellationToken);

            var result = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                if (!cinemas.TryGetValue(ticket.CinemaId, out var cinema) ||
                !clients.TryGetValue(ticket.ClientId, out var client) ||
                !films.TryGetValue(ticket.FilmId, out var film) ||
                !halls.TryGetValue(ticket.HallId, out var hall))
                {
                    continue;
                }
                else
                {
                    var ticketModel = mapper.Map<TicketModel>(ticket);

                    ticketModel.Hall = mapper.Map<HallModel>(hall);
                    ticketModel.Film = mapper.Map<FilmModel>(film);
                    ticketModel.Staff = ticket.StaffId.HasValue &&
                                              staffs.TryGetValue(ticket.StaffId!.Value, out var staff)
                        ? mapper.Map<StaffModel>(staff)
                        : null;
                    ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
                    ticketModel.Client = mapper.Map<ClientModel>(client);

                    result.Add(ticketModel);
                }
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
            var client = await clientReadRepository.GetByIdAsync(item.ClientId, cancellationToken);
            var ticketModel = mapper.Map<TicketModel>(item);

            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Client = mapper.Map<ClientModel>(client);
            ticketModel.Staff = item.StaffId.HasValue ?
                            mapper.Map<StaffModel>(await staffReadRepository.GetByIdAsync(item.StaffId.Value, cancellationToken))
                            : null;

            return ticketModel;
        }
    }
}
