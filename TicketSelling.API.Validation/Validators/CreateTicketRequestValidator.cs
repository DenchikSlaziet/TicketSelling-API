using FluentValidation;
using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketRequestValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .When(x => x.StaffId.HasValue);

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.CinemaId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.FilmId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => (int)x.Place)
                .InclusiveBetween(1, 200).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => (int)x.Row)
                .InclusiveBetween(1, 50).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(100, 5000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
