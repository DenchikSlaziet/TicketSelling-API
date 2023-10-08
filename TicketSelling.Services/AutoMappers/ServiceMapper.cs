﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Models;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.Services.Contracts.Enums;

namespace TicketSelling.Services.AutoMappers
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper() 
        {
            CreateMap<Post, PostModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Hall, HallModel>(MemberList.Destination);
            CreateMap<Person, PersonModel>(MemberList.Destination);
            CreateMap<Film, FilmModel>(MemberList.Destination);
            CreateMap<Client, ClientModel>(MemberList.Destination);
            CreateMap<Cinema, CinemaModel>(MemberList.Destination);
            CreateMap<Staff, StaffModel>(MemberList.Destination);
            CreateMap<Ticket, TicketModel>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Cinema, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore());
        }
    }
}
