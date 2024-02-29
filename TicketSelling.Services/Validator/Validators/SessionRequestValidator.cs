using FluentValidation;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Validator.Validators
{
    public class SessionRequestValidator : AbstractValidator<SessionRequestModel>
    {
        private readonly IHallReadRepository hallReadRepository;
        private readonly IFilmReadRepository filmReadRepository;

        public SessionRequestValidator(IFilmReadRepository filmReadRepository,
            IHallReadRepository hallReadRepository)
        {
            this.hallReadRepository = hallReadRepository;
            this.filmReadRepository = filmReadRepository;


            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.hallReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.FilmId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.filmReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);
          
            RuleFor(x => x.StartDateTime)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddHours(5)).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.EndDateTime)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddHours(6)).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
