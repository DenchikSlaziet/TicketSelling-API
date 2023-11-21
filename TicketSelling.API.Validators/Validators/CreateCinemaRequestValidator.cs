using FluentValidation;
using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateCinemaRequestValidator : AbstractValidator<CreateCinemaRequest>
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
