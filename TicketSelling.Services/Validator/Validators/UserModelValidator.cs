using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="UserModel"/>
    /// </summary>
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Patronymic)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 99).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(MessageForValidation.DefaultMessage).When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Login)
                .Length(5, 100).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Password)
                .Length(5, 200).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Role).IsInEnum().WithMessage(MessageForValidation.DefaultMessage);

        }
    }
}
