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
    public class StaffServiceTest : TicketSellingContextInMemory
    {
        private readonly IStaffService staffService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StaffServiceTest"/>
        /// </summary>
        public StaffServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            staffService = new StaffService(new StaffWriteRepository(WriterContext), new StaffReadRepository(Reader),
                UnitOfWork, config.CreateMapper());
        }

        /// <summary>
        /// Получение <see cref="Staff"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => staffService.GetByIdAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Staff>>(result);
        }

        /// <summary>
        /// Получение <see cref="Staff"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await staffService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Age,
                    target.FirstName,
                    target.LastName,
                    target.Patronymic
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Staff}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await staffService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Staff"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Staff();

            await Context.Staffs.AddRangeAsync(target,
                TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await staffService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => staffService.DeleteAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Staff>>(result);
        }

        /// <summary>
        /// Удаление удаленного <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Staff(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Staffs.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => staffService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }
    }
}
