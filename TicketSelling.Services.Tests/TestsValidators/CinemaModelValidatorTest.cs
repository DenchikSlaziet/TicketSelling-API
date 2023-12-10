using FluentValidation.TestHelper;
using TicketSelling.Services.Validator.Validators;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class CinemaModelValidatorTest
    {
        private readonly CinemaModelValidator validator;

        public CinemaModelValidatorTest()
        {
            validator = new CinemaModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.CinemaModel(x => { x.Title = "1"; x.Address = "1"; });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CinemaModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
