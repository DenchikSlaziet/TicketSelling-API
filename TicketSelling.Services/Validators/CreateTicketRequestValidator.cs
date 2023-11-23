using FluentValidation;
using FluentValidation.Results;
using System.Collections.Immutable;
using TicketSelling.General;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.API.Validation.Validators
{
    public class CreateTicketRequestValidator : AbstractValidator<TicketRequestModel>
    {
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;

        public CreateTicketRequestValidator(ICinemaReadRepository cinemaReadRepository, IClientReadRepository clientReadRepository,
            IFilmReadRepository filmReadRepository, IHallReadRepository hallReadRepository)
        {
            this.cinemaReadRepository = cinemaReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;

            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .When(x => x.StaffId.HasValue);

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.hallReadRepository.GetByIdAsync(x, cancellationToken) != null)
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.CinemaId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.cinemaReadRepository.GetByIdAsync(x, cancellationToken) != null)
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.clientReadRepository.GetByIdAsync(x, cancellationToken) != null)
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.FilmId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.filmReadRepository.GetByIdAsync(x, cancellationToken) != null)
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

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
