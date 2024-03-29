﻿using FluentAssertions;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.Contracts.ReadRepositiriesContracts;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Repositories.Tests.Tests
{
    public class SessionReadTests : TicketSellingContextInMemory
    {
        private readonly ISessionReadRepository cinemaReadRepository;

        public SessionReadTests()
        {
            cinemaReadRepository = new SessionReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список киносеансов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await cinemaReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список киносеансов
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
            var result = await cinemaReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение киносеанса по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await cinemaReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение киносеанса по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.Now);
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение киносеанса по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetNotDeletedByIdShouldReturnNull()
        {
            //Arrange
            var target = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.Now);
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaReadRepository.GetNotDeletedByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение киносеанса по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetNotDeletedByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Session();
            await Context.Sessions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaReadRepository.GetNotDeletedByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка киносеансов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await cinemaReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка киносеансов по идентификаторам возвращает данные
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
            var result = await cinemaReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск киносеанса в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Session();           
            await Context.Sessions.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cinemaReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск киносеанса в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await cinemaReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного киносеанса в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Sessions.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);           

            // Act
            var result = await cinemaReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
