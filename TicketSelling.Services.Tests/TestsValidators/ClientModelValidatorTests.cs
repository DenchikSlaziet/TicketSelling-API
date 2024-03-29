﻿using FluentValidation.TestHelper;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Validator.Validators;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class ClientModelValidatorTests
    {
        private readonly UserModelValidator validator;

        public ClientModelValidatorTests()
        {
            validator = new UserModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => 
            {   x.FirstName = "1"; 
                x.LastName = "1"; 
                x.Patronymic = "1";
                x.Age = -1; 
                x.Email = "1";
                x.Login = "1";
                x.Password = "1";
                x.Role = (RoleModel)56;
            });

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
            var model = TestDataGenerator.ClientModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
