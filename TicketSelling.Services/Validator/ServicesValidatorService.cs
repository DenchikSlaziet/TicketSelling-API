﻿using FluentValidation;
using TicketSelling.General;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Validator.Validators;

namespace TicketSelling.Services.Validator
{
    public sealed class ServicesValidatorService : IServiceValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;

        public ServicesValidatorService(ICinemaReadRepository cinemaReadRepository, IClientReadRepository clientReadRepository,
            IFilmReadRepository filmReadRepository, IHallReadRepository hallReadRepository)
        {
            this.cinemaReadRepository = cinemaReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;

            validators.Add(typeof(CinemaModel), new CreateCinemaRequestValidator());
            validators.Add(typeof(ClientModel), new CreateClientRequestValidator());
            validators.Add(typeof(FilmModel), new CreateFilmRequestValidator());
            validators.Add(typeof(HallModel), new CreateHallRequestValidator());
            validators.Add(typeof(StaffModel), new CreateStaffRequestValidator());
            validators.Add(typeof(TicketRequestModel), new CreateTicketRequestValidator(cinemaReadRepository, 
                clientReadRepository, filmReadRepository, hallReadRepository));
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
                throw new TimeTableValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}