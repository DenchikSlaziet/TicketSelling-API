using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.Validation.Validators
{
    /// <summary>
    /// Валидатор <see cref="HallModel"/>
    /// </summary>
    public class CreateHallRequestValidator : AbstractValidator<HallModel>
    {
        public CreateHallRequestValidator()
        {
            RuleFor(x => (int)x.Number)
              .InclusiveBetween(1, 99).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => (int)x.NumberOfSeats)
              .InclusiveBetween(15, 200).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
