using FluentValidation.TestHelper;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Services.Validator.Validators;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class SessionRequestValidatorTests : TicketSellingContextInMemory
    {
        private readonly SessionRequestValidator validator;

        public SessionRequestValidatorTests()
        {
            validator = new SessionRequestValidator(new FilmReadRepository(Reader), new HallReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.SessionRequestModel(x =>
            {
                x.StartDateTime = DateTimeOffset.Now.AddDays(-2);
                x.EndDateTime = DateTimeOffset.Now.AddDays(-1);
            });

            model.HallId = Guid.NewGuid();
            model.FilmId = Guid.NewGuid();

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

            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var session = TestDataGenerator.SessionRequestModel();
            session.HallId = hall.Id;
            session.FilmId = film.Id;

            // Act
            var result = await validator.TestValidateAsync(session);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
