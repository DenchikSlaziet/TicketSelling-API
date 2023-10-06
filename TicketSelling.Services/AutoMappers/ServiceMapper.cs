using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Enums;
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
            CreateMap<Client, ClientModel>(MemberList.Destination);
            CreateMap<Cinema, CinemaModel>(MemberList.Destination);
            CreateMap<Staff, StaffModel>(MemberList.Destination)
                .ForMember(x => x.Post, opt => opt.MapFrom(src => (Post)(int)src.Post));
        }
    }
}
