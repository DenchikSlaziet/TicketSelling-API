using AutoMapper;
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

namespace TicketSelling.Services.Services
{
    public class SessionService : ISessionService, IServiceAnchor
    {
        private readonly ISessionReadRepository sessionReadRepository;
        private readonly ISessionWriteRepository sessionWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IServiceValidatorService validatorService;

        public SessionService(ISessionReadRepository sessionReadRepository, ISessionWriteRepository sessionWriteRepository,
                IMapper mapper, IUnitOfWork unitOfWork, IHallReadRepository hallReadRepository, IFilmReadRepository filmReadRepository,
                IServiceValidatorService validatorService)
        {
            this.sessionReadRepository = sessionReadRepository;
            this.sessionWriteRepository = sessionWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.hallReadRepository = hallReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.validatorService = validatorService;
        }


        async Task<SessionModel> ISessionService.AddAsync(SessionRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var session = mapper.Map<Session>(model);
            sessionWriteRepository.Add(session);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var sessionModel = mapper.Map<SessionModel>(session);
            var hall = await hallReadRepository.GetNotDeletedByIdAsync(model.HallId, cancellationToken);
            var film = await filmReadRepository.GetNotDeletedByIdAsync(model.FilmId, cancellationToken);
            sessionModel.Hall = mapper.Map<HallModel>(hall);
            sessionModel.Film = mapper.Map<FilmModel>(film);

            return sessionModel;
        }

        async Task ISessionService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await sessionReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if(item == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(id);
            }

            sessionWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync();        
        }

        async Task<SessionModel> ISessionService.EditAsync(SessionRequestModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var session= await sessionReadRepository.GetNotDeletedByIdAsync(source.Id, cancellationToken);

            if(session == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(source.Id);
            }

            session = mapper.Map<Session>(source);
            sessionWriteRepository.Update(session);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // TODO: Не могут быть удаленные т.к. source проходит валидацию
            var sessionModel = mapper.Map<SessionModel>(session);
            var hall = await hallReadRepository.GetNotDeletedByIdAsync(source.HallId, cancellationToken);
            var film = await filmReadRepository.GetNotDeletedByIdAsync(source.FilmId, cancellationToken);
            sessionModel.Hall = mapper.Map<HallModel>(hall);
            sessionModel.Film = mapper.Map<FilmModel>(film);

            return sessionModel;
        }

        async Task<IEnumerable<SessionModel>> ISessionService.GetAllAsync(CancellationToken cancellationToken)
        {
            var sessions = await sessionReadRepository.GetAllAsync(cancellationToken);

            var filmsIds = await filmReadRepository.GetNotDeletedByIdsAsync(sessions.Select(x => x.FilmId).Distinct(), cancellationToken);
            var hallsIds = await hallReadRepository.GetNotDeletedByIdsAsync(sessions.Select(x => x.HallId).Distinct(), cancellationToken);
            var sessionModels = new List<SessionModel>(sessions.Count);

            // TODO: Запрещен вывод сеанса с удаленными залом либо фильмом
            foreach (var item in sessions)
            {
                if(!filmsIds.TryGetValue(item.FilmId, out var film) ||
                    !hallsIds.TryGetValue(item.HallId, out var hall))
                {
                    continue;
                }

                var sessionModel = mapper.Map<SessionModel>(item);
                sessionModel.Hall = mapper.Map<HallModel>(hall);
                sessionModel.Film = mapper.Map<FilmModel>(film);

                sessionModels.Add(sessionModel);
            }

            return sessionModels;
        }

        async Task<SessionModel?> ISessionService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await sessionReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if(item == null)
            {
                throw new TicketSellingEntityNotFoundException<Session>(id);
            }

            // TODO: Разрешен вывод сеанса с удаленными залом либо фильмом
            var sessionModel = mapper.Map<SessionModel>(item);
            var hall = await hallReadRepository.GetNotDeletedByIdAsync(item.HallId, cancellationToken);
            var film = await filmReadRepository.GetNotDeletedByIdAsync(item.FilmId, cancellationToken);
            sessionModel.Hall = mapper.Map<HallModel>(hall);
            sessionModel.Film = mapper.Map<FilmModel>(film);

            return sessionModel;
        }
    }
}
