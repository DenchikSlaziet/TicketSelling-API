using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.API.Enums;
using TicketSelling.API.Models;
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
            CreateMap<ClientModel, ClientResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<FilmModel, FilmResponse>(MemberList.Destination);
            CreateMap<HallModel, HallResponse>(MemberList.Destination);
            CreateMap<StaffModel, StaffResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<TicketModel, TicketResponse>(MemberList.Destination)                
                .ForMember(x => x.NameStuff, opt => opt.MapFrom(src => src.Staff != null ? $"{src.Staff.LastName} " +
                $"{src.Staff.FirstName} {src.Staff.Patronymic}" : string.Empty))
                .ForMember(x => x.Post, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.Post : PostModel.None))
                .ForMember(x => x.NumberHall, opt => opt.MapFrom(src => src.Hall != null ? src.Hall.Number : 0))
                .ForMember(x => x.CinemaAdress, opt => opt.MapFrom(src => src.Cinema != null ? src.Cinema.Address : string.Empty))
                .ForMember(x => x.CinemaName, opt => opt.MapFrom(src => src.Cinema != null ? src.Cinema.Title : string.Empty))
                .ForMember(x => x.CountHall, opt => opt.MapFrom(src => src.Hall != null ? src.Hall.NumberOfSeats : 0))
                .ForMember(x => x.FilmName, opt => opt.MapFrom(src => src.Film != null ? src.Film.Title : string.Empty))
                .ForMember(x => x.LimitationFilm, opt => opt.MapFrom(src => src.Film != null ? src.Film.Limitation : 0))
                .ForMember(x => x.NameClient, opt => opt.MapFrom(src => src.Client != null ? $"{src.Client.LastName} " +
                $"{src.Client.FirstName} {src.Client.Patronymic}" : string.Empty));
        }
    }
}
