using AutoMapper;
using FluentAssertions;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Repositories.WriteRepositoriеs;
using TicketSelling.Services.AutoMappers;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;
using TicketSelling.Services.Validator;
using TicketSelling.Services.Validator.Validators;
using Xunit;

namespace TicketSelling.Services.Tests.Tests
{
    public class ClientServiceTest : TicketSellingContextInMemory
    {
        private readonly IClientService clientService;
        private readonly ClientReadRepository clientReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientServiceTest"/>
        /// </summary>
        public ClientServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            clientReadRepository = new ClientReadRepository(Reader);

            clientService = new ClientService(new ClientWriteRepository(WriterContext), clientReadRepository,
                UnitOfWork, config.CreateMapper(), new ServicesValidatorService(new CinemaReadRepository(Reader), clientReadRepository, new FilmReadRepository(Reader),
                new HallReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Client"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.GetByIdAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Client>>(result);
        }

        /// <summary>
        /// Получение <see cref="Client"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Client();
            await Context.Clients.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Age,
                    target.FirstName,
                    target.LastName,
                    target.Email,
                    target.Patronymic
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Client}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Client}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Client();

            await Context.Clients.AddRangeAsync(target,
                TestDataGenerator.Client(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.DeleteAsync(id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableEntityNotFoundException<Client>>(result);
        }

        /// <summary>
        /// Удаление удаленного <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Client(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Clients.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => clientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await Assert.ThrowsAsync<TimeTableInvalidOperationException>(result);
        }
    }
}
