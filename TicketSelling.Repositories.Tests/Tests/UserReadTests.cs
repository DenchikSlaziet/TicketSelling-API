using FluentAssertions;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Repositories.Tests.Tests
{
    public class UserReadTests : TicketSellingContextInMemory
    {
        private readonly IUserReadRepository userReadRepository;

        public UserReadTests()
        {
            userReadRepository = new UserReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список клиентов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await userReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список клиентов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Session();

            await Context.Sessions.AddRangeAsync(target,
                TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await userReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetNotDeletedByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Session();
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.GetNotDeletedByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetNotDeletedByIdShouldReturnNull()
        {
            //Arrange
            var target = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.Now);
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.GetNotDeletedByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.Now);
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка клиентов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await userReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка клиентов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Session();
            var target2 = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Session();
            var target4 = TestDataGenerator.Session();
            await Context.Sessions.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id)
                .And.ContainKey(target2.Id);
        }

        /// <summary>
        /// Поиск клиента в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Session();
            await Context.Sessions.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск клиента в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await userReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного клиента в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Sessions.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await userReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
