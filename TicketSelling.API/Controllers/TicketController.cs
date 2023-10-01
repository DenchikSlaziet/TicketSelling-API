﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с билетами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;

        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await ticketService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<TicketResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Персонала с таким Id нет!");
            }

            return Ok(mapper.Map<TicketResponse>(item));
        }
    }
}
