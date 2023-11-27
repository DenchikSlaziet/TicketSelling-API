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
    public class FilmServiceTest : TicketSellingContextInMemory
    {
        private readonly IFilmService filmService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="FilmServiceTest"/>
        /// </summary>
        public FilmServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            filmService = new FilmService(new FilmWriteRepository(WriterContext), new FilmReadRepository(Reader),
                config.CreateMapper(), UnitOfWork);
        }

        /// <summary>
        /// Получение <see cref="Film"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => filmService.GetByIdAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Film>>(result);
        }

        /// <summary>
        /// Получение <see cref="Film"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Film();
            await Context.Films.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await filmService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Description
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Film}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await filmService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Film"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Film();

            await Context.Films.AddRangeAsync(target,
                TestDataGenerator.Film(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await filmService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Film"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => filmService.DeleteAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Film>>(result);
        }

        /// <summary>
        /// Удаление удаленного <see cref="Film"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Film(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Films.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => filmService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }
    }
}
