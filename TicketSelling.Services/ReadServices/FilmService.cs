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

        public FilmService(IFilmReadRepository filmReadRepository)
        {
            this.filmReadRepository = filmReadRepository;
        }

        async Task<IEnumerable<FilmModel>> IFilmService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await filmReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new FilmModel
            {
                Id = x.Id,
                Description = x.Description,
                Limitation = x.Limitation,
                Title = x.Title
            });
        }

        async Task<FilmModel?> IFilmService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                return null;
            }

            return new FilmModel
            {
                Id = item.Id,
                Description = item.Description,
                Limitation = item.Limitation,
                Title = item.Title
            };
        }
    }
}
