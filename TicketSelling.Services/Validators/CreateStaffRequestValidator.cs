using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateStaffRequestValidator : AbstractValidator<StaffModel>
    {
        public CreateStaffRequestValidator()
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

            RuleFor(x => (int)x.Age)
              .InclusiveBetween(18, 99).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Post).IsInEnum().WithMessage(MessageForValidation.DefaultMessage);
        }
    }
}
