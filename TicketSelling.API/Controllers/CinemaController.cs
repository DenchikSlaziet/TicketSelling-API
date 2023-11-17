﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с кинотеатрами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(ICinemaService cinemaService, IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список кинотеатров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaResponse>), StatusCodes.Status200OK)]      
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cinemaService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CinemaResponse>(x)));
        }

        /// <summary>
        /// Получить кинотетар по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
        {
            var item = await cinemaService.GetByIdAsync(id,cancellationToken);

            if(item == null)
            {
                return NotFound("Кинотеатра с таким Id нет!");
            }
            return Ok(mapper.Map<CinemaResponse>(item));
        }

        /// <summary>
        /// Добавить кинотеатр
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateCinemaRequest model, CancellationToken cancellationToken)
        {
            var result = await cinemaService.AddAsync(model.Address, model.Title, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        /// <summary>
        /// Изменить кинотеатр
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, CreateCinemaRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CinemaModel>(request);
            model.Id = id;
            var result = await cinemaService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        /// <summary>
        /// Удалить Кинотеатр по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await cinemaService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
