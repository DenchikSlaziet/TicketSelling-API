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
    public class HallServiceTest : TicketSellingContextInMemory
    {
        private readonly IHallService hallService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="HallServiceTest"/>
        /// </summary>
        public HallServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            hallService = new HallService(new HallWriteRepository(WriterContext), new HallReadRepository(Reader),
                UnitOfWork, config.CreateMapper());
        }

        /// <summary>
        /// Получение <see cref="Hall"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => hallService.GetByIdAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Hall>>(result);
        }

        /// <summary>
        /// Получение <see cref="Hall"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Hall();
            await Context.Halls.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await hallService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.NumberOfSeats,
                    target.Number
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Hall}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await hallService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Hall}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Hall();

            await Context.Halls.AddRangeAsync(target,
                TestDataGenerator.Hall(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await hallService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Hall"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => hallService.DeleteAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Hall>>(result);
        }

        /// <summary>
        /// Удаление удаленного <see cref="Hall"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Hall(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Halls.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => hallService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }
    }
}
