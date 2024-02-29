using AutoMapper;
using FluentValidation;
using System.Net.Sockets;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.ReadRepositiriesContracts;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.Services.ReadServices
{
    /// <inheritdoc cref="ITicketService"/>
    public class TicketService : ITicketService, IServiceAnchor
    {
        private readonly ITicketWriteRepository ticketWriteRepository;
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly IUserReadRepository userReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly ISessionReadRepository sessionReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public TicketService(ITicketWriteRepository ticketWriteRepository, ITicketReadRepository ticketReadRepository,
            IUserReadRepository userReadRepository, IFilmReadRepository filmReadRepository,
            IHallReadRepository hallReadRepository, IStaffReadRepository staffReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService,
            ISessionReadRepository sessionReadRepository)
        {
            this.ticketWriteRepository = ticketWriteRepository;
            this.ticketReadRepository = ticketReadRepository;
            this.userReadRepository = userReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
            this.sessionReadRepository = sessionReadRepository;
        }

        async Task<TicketModel> ITicketService.AddAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var ticket = mapper.Map<Ticket>(model);      
            ticketWriteRepository.Add(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketModel = mapper.Map<TicketModel>(ticket);
            var session = await sessionReadRepository.GetByIdAsync(model.SessionId, cancellationToken);

            if (session == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(model.SessionId);
            }

            var staff = model.StaffId.HasValue
                ? await staffReadRepository.GetByIdAsync(model.StaffId.Value, cancellationToken)
                : null;
            var user = await userReadRepository.GetByIdAsync(model.UserId, cancellationToken);
            var hall = await hallReadRepository.GetByIdAsync(session!.HallId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(session!.FilmId, cancellationToken);
            var sessionModel = mapper.Map<SessionModel>(session);

            sessionModel.Film = mapper.Map<FilmModel>(film);
            sessionModel.Hall = mapper.Map<HallModel>(hall);

            ticketModel.Session = sessionModel;
            ticketModel.Staff = mapper.Map<StaffModel>(staff);
            ticketModel.User = mapper.Map<UserModel>(user);

            return ticketModel;
        }

        async Task ITicketService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTicket = await ticketReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if (targetTicket == null)
            {
                throw new TicketSellingEntityNotFoundException<Ticket>(id);
            }

            ticketWriteRepository.Delete(targetTicket);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TicketModel> ITicketService.EditAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var ticket = await ticketReadRepository.GetNotDeletedByIdAsync(model.Id, cancellationToken);

            if (ticket == null)
            {
                throw new TicketSellingEntityNotFoundException<Ticket>(model.Id);
            }

            ticket = mapper.Map<Ticket>(model);       
            ticketWriteRepository.Update(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketModel = mapper.Map<TicketModel>(ticket);
            var session = await sessionReadRepository.GetByIdAsync(model.SessionId, cancellationToken);

            if (session == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(model.SessionId);
            }

            var staff = model.StaffId.HasValue
                ? await staffReadRepository.GetByIdAsync(model.StaffId.Value, cancellationToken)
                : null;
            var user = await userReadRepository.GetByIdAsync(model.UserId, cancellationToken);
            var hall = await hallReadRepository.GetByIdAsync(session!.HallId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(session!.FilmId, cancellationToken);
            var sessionModel = mapper.Map<SessionModel>(session);

            sessionModel.Film = mapper.Map<FilmModel>(film);
            sessionModel.Hall = mapper.Map<HallModel>(hall);

            ticketModel.Session = sessionModel;
            ticketModel.Staff = mapper.Map<StaffModel>(staff);
            ticketModel.User = mapper.Map<UserModel>(user);

            return ticketModel;
        }

        //TODO: Вывожу не удаленные, но с информацией возможно удаленной
        async Task<IEnumerable<TicketModel>> ITicketService.GetAllAsync(CancellationToken cancellationToken)
        {
            var tickets = await ticketReadRepository.GetAllAsync(cancellationToken);

            var userIds = await userReadRepository
                .GetByIdsAsync(tickets.Select(x => x.UserId).Distinct(), cancellationToken);

            var staffIds = await staffReadRepository
                .GetByIdsAsync(tickets.Where(x => x.StaffId.HasValue).Select(x => x.StaffId!.Value).Distinct(), cancellationToken);

            var sessionIds = await sessionReadRepository
                .GetByIdsAsync(tickets.Select(x => x.SessionId).Distinct(), cancellationToken);

            var result = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                if (!userIds.TryGetValue(ticket.UserId, out var user) ||
                    !sessionIds.TryGetValue(ticket.SessionId, out var session))
                {
                    continue;
                }
                else
                {
                    var ticketModel = mapper.Map<TicketModel>(ticket);
                   
                    var hall = await hallReadRepository.GetByIdAsync(session!.HallId, cancellationToken);
                    var film = await filmReadRepository.GetByIdAsync(session!.FilmId, cancellationToken);
                    var sessionModel = mapper.Map<SessionModel>(session);
                    var staffModel = ticket.StaffId.HasValue && staffIds.TryGetValue(ticket.StaffId.Value, out var staff)
                        ? mapper.Map<StaffModel>(staff) : null;

                    sessionModel.Film = mapper.Map<FilmModel>(film);
                    sessionModel.Hall = mapper.Map<HallModel>(hall);

                    ticketModel.Session = sessionModel;
                    ticketModel.Staff = staffModel;
                    ticketModel.User = mapper.Map<UserModel>(user);

                    result.Add(ticketModel);
                }
            }

            return result;
        }

        //TODO: Вывожу не удаленные, но с информацией возможно удаленной
        async Task<TicketModel?> ITicketService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new TicketSellingEntityNotFoundException<Ticket>(id);
            }

            var ticketModel = mapper.Map<TicketModel>(item);

            var session = await sessionReadRepository.GetByIdAsync(item.SessionId, cancellationToken);

            if(session == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(item.SessionId);
            }

            var hall = await hallReadRepository.GetByIdAsync(session!.HallId, cancellationToken);
            var film = await filmReadRepository.GetByIdAsync(session!.FilmId, cancellationToken);
            var sessionModel = mapper.Map<SessionModel>(session);
            var staff = item.StaffId.HasValue
                ? await staffReadRepository.GetByIdAsync(item.StaffId.Value, cancellationToken)
                : null;
            var user = await userReadRepository.GetByIdAsync(item.UserId, cancellationToken);

            sessionModel.Film = mapper.Map<FilmModel>(film);
            sessionModel.Hall = mapper.Map<HallModel>(hall);
         
            ticketModel.Session = sessionModel;
            ticketModel.Staff = mapper.Map<StaffModel>(staff);
            ticketModel.User = mapper.Map<UserModel>(user);

            return ticketModel;
        }        
    }
}
