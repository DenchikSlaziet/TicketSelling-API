using FluentValidation;
using TicketSelling.General;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.ReadRepositiriesContracts;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Contracts.ServicesContracts;
using TicketSelling.Services.Validator.Validators;

namespace TicketSelling.Services.Services
{
    public sealed class ServicesValidatorService : IServiceValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ServicesValidatorService(ISessionReadRepository sessionReadRepository, IUserReadRepository userReadRepository,
            IFilmReadRepository filmReadRepository, IHallReadRepository hallReadRepository, IStaffReadRepository staffReadRepository)
        {
            validators.Add(typeof(SessionRequestModel), new SessionRequestValidator(filmReadRepository, hallReadRepository));
            validators.Add(typeof(UserModel), new UserModelValidator());
            validators.Add(typeof(FilmModel), new FilmModelValidator());
            validators.Add(typeof(HallModel), new HallModelValidator());
            validators.Add(typeof(StaffModel), new StaffModelValidator());
            validators.Add(typeof(TicketRequestModel), new TicketRequestValidator(userReadRepository, sessionReadRepository,
               staffReadRepository));
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TicketSellingValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
