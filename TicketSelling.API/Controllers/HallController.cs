using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с залами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await hallService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<HallResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await hallService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Зала с таким Id нет!");
            }
            return Ok(mapper.Map<HallResponse>(item));
        }
    }
}
