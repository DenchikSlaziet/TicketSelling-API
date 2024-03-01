using AutoMapper;
using FluentAssertions;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Repositories.WriteRepositoriеs;
using TicketSelling.Services.AutoMappers;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.ServicesContracts;
using TicketSelling.Services.ReadServices;
using TicketSelling.Services.Services;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.Tests
{
    public class TicketServiceTests : TicketSellingContextInMemory
    {
        private readonly ITicketService ticketService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketServiceTests"/>
        /// </summary>
        public TicketServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            ticketService = new TicketService(new TicketWriteRepository(WriterContext), new TicketReadRepositiry(Reader),
                new UserReadRepository(Reader), new FilmReadRepository(Reader),
                new HallReadRepository(Reader), new StaffReadRepository(Reader), config.CreateMapper(), UnitOfWork,
                new ServicesValidatorService(new SessionReadRepository(Reader),
                new UserReadRepository(Reader), new FilmReadRepository(Reader), new HallReadRepository(Reader), new StaffReadRepository(Reader)),
                new SessionReadRepository(Reader));
        }

        /// <summary>
        /// Получение <see cref="Ticket"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => ticketService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Ticket>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Ticket"/> по идентификатору возвращает данные
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

            var target = TestDataGenerator.Ticket();
            target.SessionId = session.Id;

            await Context.Halls.AddAsync(hall);
            await Context.Films.AddAsync(film);
            await Context.Sessions.AddAsync(session);
            await Context.Tickets.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await ticketService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.DatePayment,
                    target.Row,
                    target.Place,
                    target.Price
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Ticket}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await ticketService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Ticket}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Ticket();         
            await Context.Tickets.AddRangeAsync(target,
                TestDataGenerator.Ticket(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await ticketService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => ticketService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Ticket>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Ticket(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Tickets.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => ticketService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<Ticket>>()
                .WithMessage($"*{model.Id}*");
        }
        
        /// <summary>
        /// Удаление <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Ticket();
            await Context.Tickets.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => ticketService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tickets.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Users.AddAsync(user);
            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Sessions.AddAsync(session);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.TicketRequestModel();
            model.UserId = user.Id;
            model.SessionId = session.Id;

            //Act
            Func<Task> act = () => ticketService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tickets.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.TicketRequestModel();

            //Act
            Func<Task> act = () => ticketService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Users.AddAsync(user);
            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Sessions.AddAsync(session);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.TicketRequestModel();
            model.UserId = user.Id;
            model.SessionId = session.Id;
            //Act
            Func<Task> act = () => ticketService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingEntityNotFoundException<Ticket>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.TicketRequestModel();

            //Act
            Func<Task> act = () => ticketService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var session = TestDataGenerator.Session();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await Context.Users.AddAsync(user);
            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Sessions.AddAsync(session);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var ticket = TestDataGenerator.Ticket();
            ticket.UserId = user.Id;
            ticket.SessionId = session.Id;

            var model = TestDataGenerator.TicketRequestModel(x => x.Id = ticket.Id);
            model.UserId = ticket.UserId;
            model.SessionId = ticket.SessionId;

            await Context.Tickets.AddAsync(ticket, CancellationToken);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => ticketService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tickets.Single(x => x.Id == ticket.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Place,
                    model.Price,
                    model.Row,
                    model.DatePayment
                });
        }
    }
}
