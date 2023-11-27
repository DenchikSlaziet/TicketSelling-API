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
    public class CinemaServiceTest : TicketSellingContextInMemory
    {
        private readonly ICinemaService cinemaeService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CinemaServiceTest"/>
        /// </summary>
        public CinemaServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            cinemaeService = new CinemaService(new CinemaReadRepository(Reader), config.CreateMapper(), 
                new CinemaWriteRepository(WriterContext), UnitOfWork);
        }

        /// <summary>
        /// Получение <see cref="Cinema"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => cinemaeService.GetByIdAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Cinema>>(result);
        }

        /// <summary>
        /// Получение <see cref="Cinema"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Cinema();
            await Context.Cinemas.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaeService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Address
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Cinema}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await cinemaeService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Cinema}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Cinema();

            await Context.Cinemas.AddRangeAsync(target,
                TestDataGenerator.Cinema(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaeService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Cinema"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => cinemaeService.DeleteAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Cinema>>(result);
        }

        /// <summary>
        /// Удаление удаленного <see cref="Cinema"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Cinema(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Cinemas.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => cinemaeService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }

        ///// <summary>
        ///// Удаление кинотеатра
        ///// </summary>
        //[Fact]
        //public async Task DeletingCinemaReturnTrue()
        //{
        //    //Arrange
        //    var model = TestDataGenerator.Cinema();
        //    await Context.Cinemas.AddAsync(model);
        //    await Context.SaveChangesAsync(CancellationToken);

        //    //Act
        //    var result = await Context.Cinemas.FirstAsync();
        //    await cinemaeService.DeleteAsync(result.Id, CancellationToken);

        //    // Assert
        //    result.DeletedAt.HasValue.Should().BeTrue();
        //}
    }
}
