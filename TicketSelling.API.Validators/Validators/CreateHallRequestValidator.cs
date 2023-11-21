using FluentValidation;
using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateHallRequestValidator : AbstractValidator<HallRequest>
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
