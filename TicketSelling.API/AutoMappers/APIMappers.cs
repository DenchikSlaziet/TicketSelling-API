using AutoMapper;
using TicketSelling.API.Enums;
using TicketSelling.API.Models;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.AutoMappers
{
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<CinemaModel, CinemaResponse>(MemberList.Destination);
            CreateMap<ClientModel, ClientResponse>(MemberList.Destination).
                ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<FilmModel, FilmResponse>(MemberList.Destination);
            CreateMap<HallModel, HallResponse>(MemberList.Destination);
            CreateMap<StaffModel, StaffResponse>(MemberList.Destination).
                ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}")).
                ForMember(x => x.Post, opt => opt.MapFrom(src => (Post)(int)src.Post));

            CreateMap<TicketModel, TicketResponse>(MemberList.Destination).
                ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date.ToString("MM.dd-yyyy h:m"))).
                ForMember(x => x.StuffPost, opt => opt.MapFrom(src => src.Staff == null ? null : src.Staff.Post)).
                ForMember(x => x.NameStuff, opt => opt.MapFrom(src => src.Staff == null ? null : $"{src.Staff.LastName} " +
                $"{src.Staff.FirstName} {src.Staff.Patronymic}")).
                ForMember(x => x.NumberHall, opt => opt.MapFrom(src => src.Hall == null ? 0 : src.Hall.Number)).
                ForMember(x => x.CinemaAdress, opt => opt.MapFrom(src => src.Cinema == null ? null : src.Cinema.Address)).
                ForMember(x => x.CinemaName, opt => opt.MapFrom(src => src.Cinema == null ? null : src.Cinema.Title)).
                ForMember(x => x.CountHall, opt => opt.MapFrom(src => src.Hall == null ? 0 :src.Hall.NumberOfSeats)).
                ForMember(x => x.FilmName, opt => opt.MapFrom(src => src.Film == null ? null : src.Film.Title)).
                ForMember(x => x.LimitationFilm, opt => opt.MapFrom(src => src.Film == null ? 0 : src.Film.Limitation)).
                ForMember(x => x.NameClient, opt => opt.MapFrom(src => src.Client == null ? null : $"{src.Client.LastName} " +
                $"{src.Client.FirstName} {src.Client.Patronymic}"));
        }
    }
}
