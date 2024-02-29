using AutoMapper;
using FluentAssertions;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
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
    public class UserServiceTests : TicketSellingContextInMemory
    {
        private readonly IUserService clientService;
        private readonly UserReadRepository clientReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserServiceTests"/>
        /// </summary>
        public UserServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            clientReadRepository = new UserReadRepository(Reader);

            clientService = new UserService(new UserWriteRepository(WriterContext), clientReadRepository,
                UnitOfWork, config.CreateMapper(), new ServicesValidatorService(new SessionReadRepository(Reader),
                clientReadRepository, new FilmReadRepository(Reader), new HallReadRepository(Reader), new StaffReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="User"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<User>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="User"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.User();
            await Context.Users.AddAsync(target);
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
                    target.Patronymic,
                    target.Login,
                    target.Password
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{User}"/> по идентификаторам возвращает пустую коллекцию
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
        /// Получение <see cref="IEnumerable{User}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.User();

            await Context.Users.AddRangeAsync(target,
                TestDataGenerator.User(x => x.DeletedAt = DateTimeOffset.UtcNow));
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
        /// Удаление несуществуюущего <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<User>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.User(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Users.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => clientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TicketSellingEntityNotFoundException<User>>()
                .WithMessage($"*{model.Id}*");
        }      

        /// <summary>
        /// Удаление <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.User();
            await Context.Users.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => clientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Users.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();

            //Act
            Func<Task> act = () => clientService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Users.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление невалидируемого <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => x.FirstName = "T");

            //Act
            Func<Task> act = () => clientService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingEntityNotFoundException<User>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => x.FirstName = "T");

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TicketSellingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="User"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();
            var client = TestDataGenerator.User(x => x.Id = model.Id);
            await Context.Users.AddAsync(client);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Users.Single(x => x.Id == client.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.FirstName,
                    model.LastName,
                    model.Patronymic,
                    model.Email,
                    model.Login,
                    model.Password
                });
        }
    }
}
