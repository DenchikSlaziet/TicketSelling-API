using FluentValidation.TestHelper;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Services.Validator.Validators;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class TicketRequestValidatorTests : TicketSellingContextInMemory
    {
        private readonly TicketRequestValidator validator;

        public TicketRequestValidatorTests()
        {
            validator = new TicketRequestValidator(new UserReadRepository(Reader), 
                new SessionReadRepository(Reader), new StaffReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.TicketRequestModel(x => { x.Row = -1; x.DatePayment = DateTimeOffset.Now; x.Price = 0; x.Place = -1;});
            model.UserId = Guid.NewGuid();
            model.SessionId = Guid.NewGuid();
            model.StaffId = Guid.Empty;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        async public void ValidatorShouldSuccess()
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

            var ticket = TestDataGenerator.TicketRequestModel();
            ticket.UserId = user.Id;
            ticket.SessionId = session.Id;

            // Act
            var result = await validator.TestValidateAsync(ticket);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
