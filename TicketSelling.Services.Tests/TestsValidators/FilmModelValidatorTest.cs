﻿using FluentValidation.TestHelper;
using TicketSelling.Services.Validator.Validators;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class FilmModelValidatorTest
    {
        private readonly FilmModelValidator validator;

        public FilmModelValidatorTest()
        {
            validator = new FilmModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.FilmModel(x => { x.Title = "1"; x.Limitation = 22; x.Description = "1"; });

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
            var model = TestDataGenerator.FilmModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
