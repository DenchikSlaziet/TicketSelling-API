using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.API.Enums;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Request;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.API.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<GenreModel, GenreResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<PostModel, PostResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<RoleModel, RoleResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<PaymentMethodModel, PaymentMethodResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreateFilmRequest, FilmModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateHallRequest, HallModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateUserRequest, UserModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateStaffRequest, StaffModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateTicketRequest, TicketRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateSessionRequest, SessionRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<FilmRequest, FilmModel>(MemberList.Destination).ReverseMap();
            CreateMap<HallRequest, HallModel>(MemberList.Destination).ReverseMap();
            CreateMap<UserRequest, UserModel>(MemberList.Destination).ReverseMap();
            CreateMap<StaffRequest, StaffModel>(MemberList.Destination).ReverseMap();
            CreateMap<TicketRequest, TicketModel>(MemberList.Destination)
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Session, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore()).ReverseMap();

            CreateMap<SessionRequest, SessionModel>(MemberList.Destination)
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.Hall, opt => opt.Ignore()).ReverseMap();

            CreateMap<TicketRequest, TicketRequestModel>(MemberList.Destination).ReverseMap();
            CreateMap<SessionRequest, SessionRequestModel>(MemberList.Destination).ReverseMap();

            CreateMap<FilmModel, FilmResponse>(MemberList.Destination);
            CreateMap<HallModel, HallResponse>(MemberList.Destination);
            CreateMap<TicketModel, TicketResponse>(MemberList.Destination);
            CreateMap<SessionModel, SessionResponse>(MemberList.Destination);
            CreateMap<StaffModel, StaffResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));

            CreateMap<UserModel, UserResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));
        }
    }
}
