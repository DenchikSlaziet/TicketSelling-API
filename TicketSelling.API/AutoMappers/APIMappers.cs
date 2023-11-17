using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.API.Enums;
using TicketSelling.API.Models.CreateRequest;
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
            CreateMap<CreateCinemaRequest, CinemaModel>(MemberList.Destination);
            CreateMap<CreateFilmRequest, FilmModel>(MemberList.Destination);
            CreateMap<CreateHallRequest, HallModel>(MemberList.Destination);
            CreateMap<CreateClientRequest, ClientModel>(MemberList.Destination);
            CreateMap<CreateStaffRequest, StaffModel>(MemberList.Destination);
            CreateMap<CreateTicketRequest, TicketModel>(MemberList.Destination)
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
