using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class ServiceMapper : Profile
    {
        public ServiceMapper() 
        {
            CreateMap<Post, PostModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<Role, RoleModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<Genre, GenreModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Hall, HallModel>(MemberList.Destination).ReverseMap();
            CreateMap<Film, FilmModel>(MemberList.Destination).ReverseMap();
            CreateMap<User, UserModel>(MemberList.Destination).ReverseMap();
            CreateMap<Staff, StaffModel>(MemberList.Destination).ReverseMap();
            CreateMap<Ticket, TicketModel>(MemberList.Destination)
                .ForMember(x => x.Staff, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Session, opt => opt.Ignore()).ReverseMap();

            CreateMap<Session, SessionModel>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore()).ReverseMap();

            CreateMap<TicketRequestModel, Ticket>(MemberList.Destination)
                .ForMember(x => x.Session, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

            CreateMap<SessionRequestModel, Session>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());
        }
    }
}
