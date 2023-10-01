using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.AutoMappers
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper() 
        {
            CreateMap<Hall, HallModel>(MemberList.Destination);
            CreateMap<Person, PersonModel>(MemberList.Destination);
            CreateMap<Film, FilmModel>(MemberList.Destination);
            CreateMap<Cinema, CinemaModel>(MemberList.Destination);
            CreateMap<Hall, HallModel>(MemberList.Destination);
            CreateMap<Person, PersonModel>(MemberList.Destination);
        }
    }
}
