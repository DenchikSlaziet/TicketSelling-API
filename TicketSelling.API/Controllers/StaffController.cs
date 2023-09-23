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

        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new StaffResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Age = x.Age,
                LastName = x.LastName,
                Patronymic = x.Patronymic
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Персонала с таким Id нет!");
            }

            return Ok(new StaffResponse
            {
                Id = item.Id,
                FirstName = item.FirstName,
                Age = item.Age,
                LastName = item.LastName,
                Patronymic = item.Patronymic
            });
        }
    }
}
