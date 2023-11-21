using FluentValidation;
using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateFilmRequestValidator : AbstractValidator<FilmRequest>
    {
        public CreateFilmRequestValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Description)
                .Length(3,500).WithMessage(MessageForValidation.LengthMessage).When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => (int)x.Limitation)
               .InclusiveBetween(0, 18).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
