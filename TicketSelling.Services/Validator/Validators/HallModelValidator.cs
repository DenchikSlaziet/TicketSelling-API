using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="HallModel"/>
    /// </summary>
    public class HallModelValidator : AbstractValidator<HallModel>
    {
        public HallModelValidator()
        {
            RuleFor(x => x.Number)
              .InclusiveBetween(1, 999).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.CountPlaceInRow)
              .InclusiveBetween(3, 10).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.CountRow)
              .InclusiveBetween(1, 7).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
