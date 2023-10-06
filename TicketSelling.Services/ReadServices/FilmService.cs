using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class FilmService : IFilmService
    {
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IMapper mapper;

        public FilmService(IFilmReadRepository filmReadRepository, IMapper mapper)
        {
            this.filmReadRepository = filmReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<FilmModel>> IFilmService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await filmReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<FilmModel>(x));
        }

        async Task<FilmModel?> IFilmService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                return null;
            }
            return mapper.Map<FilmModel>(item);
        }
    }
}
