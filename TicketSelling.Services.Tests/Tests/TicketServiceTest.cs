using AutoMapper;
using FluentAssertions;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Repositories.WriteRepositoriеs;
using TicketSelling.Services.AutoMappers;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;
using Xunit;

namespace TicketSelling.Services.Tests.Tests
{
    public class TicketServiceTest : TicketSellingContextInMemory
    {
        private readonly ITicketService ticketService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketServiceTest"/>
        /// </summary>
        public TicketServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            ticketService = new TicketService(new TicketWriteRepository(WriterContext), new TicketReadRepositiry(Reader),
                new CinemaReadRepository(Reader), new ClientReadRepository(Reader), new FilmReadRepository(Reader),
                new HallReadRepository(Reader), new StaffReadRepository(Reader), config.CreateMapper(), UnitOfWork);
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
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Ticket>>(result);
        }

        /// <summary>
        /// Получение <see cref="Ticket"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Ticket();
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
                    target.Date,
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
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Ticket>>(result);
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
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }
    }
}
