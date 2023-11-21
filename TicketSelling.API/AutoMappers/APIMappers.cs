﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.API.Enums;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.API.AutoMappers
{
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<PostModel, PostResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<CinemaModel, CinemaResponse>(MemberList.Destination);
            CreateMap<CreateCinemaRequest, CinemaModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());

            CreateMap<CreateFilmRequest, FilmModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());

            CreateMap<CreateHallRequest, HallModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());

            CreateMap<CreateClientRequest, ClientModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());

            CreateMap<CreateStaffRequest, StaffModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());


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

            CreateMap<TicketRequest, TicketRequestModel>(MemberList.Destination);

            CreateMap<CreateTicketRequest, TicketRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());

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
