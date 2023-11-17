using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с залами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Hall")]
    public class HallController : ControllerBase
    {
        private readonly IHallService hallService;
        private readonly IMapper mapper;

        public HallController(IHallService hallService, IMapper mapper)
        {
            this.hallService = hallService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список залов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<HallResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await hallService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<HallResponse>(x)));
        }

        /// <summary>
        /// Получить зал по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await hallService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Зала с таким Id нет!");
            }
            return Ok(mapper.Map<HallResponse>(item));
        }

        /// <summary>
        /// Добавить зал
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateHallRequest model, CancellationToken cancellationToken)
        {
            var result = await hallService.AddAsync(model.Number, model.NumberOfSeats, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        /// <summary>
        /// Изменить зал по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, CreateHallRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<HallModel>(request);
            model.Id = id;
            var result = await hallService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        /// <summary>
        /// Удалить зал по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await hallService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
