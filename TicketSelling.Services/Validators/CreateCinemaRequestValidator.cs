using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.Validation.Validators
{
    /// <summary>
    /// Валидатор <see cref="CinemaModel"/>
    /// </summary>
    public class CreateCinemaRequestValidator : AbstractValidator<CinemaModel>
    {
        public CreateCinemaRequestValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(3, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(10, 100).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
