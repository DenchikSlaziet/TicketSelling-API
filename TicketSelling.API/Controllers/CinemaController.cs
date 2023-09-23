using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с кинотеатрами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cinemaService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new CinemaResponse
            {
                Id = x.Id,
                Address = x.Address,
                Title = x.Title
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
        {
            var item = await cinemaService.GetByIdAsync(id,cancellationToken);

            if(item == null)
            {
                return NotFound("Кинотеатра с таким Id нет!");
            }

            return Ok(new CinemaResponse
            {
                Id = item.Id,
                Address = item.Address, 
                Title = item.Title
            });
        }
    }
}
