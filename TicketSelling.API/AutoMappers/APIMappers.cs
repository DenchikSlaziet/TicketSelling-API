﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.API.Enums;
using TicketSelling.API.Models.Request;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.AutoMappers
{
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<PostModel, PostResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<CinemaModel, CinemaResponse>(MemberList.Destination);
            CreateMap<CinemaRequest, CinemaModel>(MemberList.Destination);
            CreateMap<FilmRequest, FilmModel>(MemberList.Destination);
            CreateMap<HallRequest, HallModel>(MemberList.Destination);
            CreateMap<ClientRequest, ClientModel>(MemberList.Destination);
            CreateMap<StaffRequest, StaffModel>(MemberList.Destination);
            CreateMap<TicketRequest, TicketModel>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Cinema, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore());

            CreateMap<ClientModel, ClientResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<FilmModel, FilmResponse>(MemberList.Destination);
            CreateMap<HallModel, HallResponse>(MemberList.Destination);
            CreateMap<StaffModel, StaffResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<TicketModel, TicketResponse>(MemberList.Destination);    
        }
    }
}
