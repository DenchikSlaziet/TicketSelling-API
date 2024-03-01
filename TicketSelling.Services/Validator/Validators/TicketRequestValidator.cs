using FluentValidation;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.ReadRepositiriesContracts;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="TicketRequestModel"/>
    /// </summary>
    public class TicketRequestValidator : AbstractValidator<TicketRequestModel>
    {
        private readonly IUserReadRepository userReadRepository;
        private readonly ISessionReadRepository sessionReadRepository;
        private readonly IStaffReadRepository staffReadRepository;

        public TicketRequestValidator(IUserReadRepository userReadRepository,
            ISessionReadRepository sessionReadRepository, IStaffReadRepository staffReadRepository)
        {
            this.userReadRepository = userReadRepository;
            this.sessionReadRepository = sessionReadRepository;
            this.staffReadRepository = staffReadRepository;

            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.staffReadRepository.IsNotNullAsync(x!.Value, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage)
                .When(x => x.StaffId.HasValue);

            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.sessionReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);           

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.userReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);         

            RuleFor(x => x.DatePayment)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddMinutes(-1)).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Place)
                .InclusiveBetween(1, 10).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Row)
                .InclusiveBetween(1, 7).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(100, 5000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
