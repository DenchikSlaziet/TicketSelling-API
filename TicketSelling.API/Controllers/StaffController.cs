using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с персоналом
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;
        private readonly IMapper mapper;

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            this.staffService = staffService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<StaffResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Персонала с таким Id нет!");
            }

            return Ok(mapper.Map<StaffResponse>(item));
        }
    }
}
