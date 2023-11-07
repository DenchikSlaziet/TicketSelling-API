using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Request;
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

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<HallResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await hallService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<HallResponse>(x)));
        }

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

        [HttpPost]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateHallRequest model, CancellationToken cancellationToken)
        {
            var result = await hallService.AddAsync(model.Number, model.NumberOfSeats, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(HallRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<HallModel>(request);
            var result = await hallService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await hallService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
