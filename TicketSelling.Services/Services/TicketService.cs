using AutoMapper;
using FluentValidation;
using TicketSelling.API.Validation.Validators;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Contracts.ReadServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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
        private readonly CreateTicketRequestValidator validations;

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
            validations = new CreateTicketRequestValidator(cinemaReadRepository, clientReadRepository, filmReadRepository, hallReadRepository);
        }

        async Task<TicketModel> ITicketService.AddAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            await validations.ValidateAndThrowAsync(model);
            var ticket = mapper.Map<Ticket>(model);

            var cinema = await cinemaReadRepository.GetByIdAsync(ticket.CinemaId, cancellationToken);
            if (cinema != null)
            {
                ticket.Cinema = cinema;
            }

            var film = await filmReadRepository.GetByIdAsync(ticket.FilmId, cancellationToken);
            if (film != null)
            {
                ticket.Film = film;
            }

            var hall = await hallReadRepository.GetByIdAsync(ticket.HallId, cancellationToken);
            if (hall != null)
            {
                ticket.Hall = hall;
            }

            var client = await clientReadRepository.GetByIdAsync(ticket.ClientId, cancellationToken);
            if (client != null)
            {
                ticket.Client = client;
            }

            ticket.Staff = ticket.StaffId.HasValue ?
                await staffReadRepository.GetByIdAsync(ticket.StaffId.Value, cancellationToken)
                : null;

            ticketWriteRepository.Add(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketModel = mapper.Map<TicketModel>(ticket);
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);
            ticketModel.Staff = mapper.Map<StaffModel>(ticket.Staff);
            ticketModel.Client = mapper.Map<ClientModel>(client);

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

        async Task<TicketModel> ITicketService.EditAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            var ticket = await ticketReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (ticket == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(model.Id);
            }

            ticket.Date = model.Date;
            ticket.Place = model.Place;
            ticket.Price = model.Price;
            ticket.Row = model.Row;

            var cinema = await cinemaReadRepository.GetByIdAsync(ticket.CinemaId, cancellationToken);
            if (cinema != null)
            {
                ticket.Cinema = cinema;
                ticket.CinemaId = model.CinemaId;
            }

            var film = await filmReadRepository.GetByIdAsync(ticket.FilmId, cancellationToken);
            if (film != null)
            {
                ticket.Film = film;
                ticket.FilmId = model.FilmId;
            }

            var hall = await hallReadRepository.GetByIdAsync(ticket.HallId, cancellationToken);
            if (hall != null)
            {
                ticket.Hall = hall;
                ticket.HallId = model.HallId;
            }

            var client = await clientReadRepository.GetByIdAsync(ticket.ClientId, cancellationToken);
            if (client != null)
            {
                ticket.Client = client;
                ticket.ClientId = model.ClientId;
            }

            ticket.StaffId = model.StaffId;
            ticket.Staff = model.StaffId.HasValue ?
                await staffReadRepository.GetByIdAsync(model.StaffId.Value, cancellationToken)
                : null;

            ticketWriteRepository.Update(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketModel = mapper.Map<TicketModel>(ticket);
            ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
            ticketModel.Hall = mapper.Map<HallModel>(hall);
            ticketModel.Film = mapper.Map<FilmModel>(film);
            ticketModel.Staff = mapper.Map<StaffModel>(ticket.Staff);
            ticketModel.Client = mapper.Map<ClientModel>(client);

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
                throw new TimeTableEntityNotFoundException<Ticket>(id);
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
