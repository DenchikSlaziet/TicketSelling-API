using AutoMapper;
using FluentAssertions;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Repositories.WriteRepositoriеs;
using TicketSelling.Services.AutoMappers;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.ServicesContracts;
using TicketSelling.Services.Services;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsServices
{
    public class SessionServiceTests : TicketSellingContextInMemory
    {
        private readonly ISessionService sessionService;

        public SessionServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            sessionService = new SessionService(new SessionReadRepository(Reader), new SessionWriteRepository(WriterContext),
                config.CreateMapper(), UnitOfWork, new HallReadRepository(Reader), new FilmReadRepository(Reader),
                new ServicesValidatorService(new SessionReadRepository(Reader),
                new UserReadRepository(Reader), new FilmReadRepository(Reader), new HallReadRepository(Reader), new StaffReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Session"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => sessionService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Session>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Session"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var session = TestDataGenerator.Session();
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();

            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Halls.AddAsync(hall);
            await Context.Films.AddAsync(film);
            await Context.Sessions.AddAsync(session);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await sessionService.GetByIdAsync(session.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    session.Id,
                    session.StartDateTime,
                    session.EndDateTime
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Session}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await sessionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Session}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Session();
            await Context.Sessions.AddRangeAsync(target,
                TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.UtcNow)); ////////
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await sessionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => sessionService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Session>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Session(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Sessions.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => sessionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Session>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Session();
            await Context.Sessions.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => sessionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Sessions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Sessions.AddAsync(session);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.SessionRequestModel();
            model.FilmId = film.Id;
            model.HallId = hall.Id;

            //Act
            Func<Task> act = () => sessionService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Sessions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.SessionRequestModel();

            //Act
            Func<Task> act = () => sessionService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.SessionRequestModel();
            model.FilmId = film.Id;
            model.HallId = hall.Id;
            //Act
            Func<Task> act = () => sessionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingEntityNotFoundException<Session>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.SessionRequestModel();

            //Act
            Func<Task> act = () => sessionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Session"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Sessions.AddAsync(session);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.SessionRequestModel(x => x.Id = session.Id);
            model.HallId = hall.Id;
            model.FilmId = film.Id;

            //Act
            Func<Task> act = () => sessionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Sessions.Single(x => x.Id == session.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.FilmId,
                    model.HallId,
                    model.StartDateTime,
                    model.EndDateTime
                });
        }
    }
}
