using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TicketSelling.API.AutoMappers;
using TicketSelling.Context;
using TicketSelling.Context.Contracts;
using TicketSelling.Repositories;
using TicketSelling.Services;
using TicketSelling.Services.AutoMappers;

namespace TicketSelling.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.RegistrationServices();
            services.RegistrationRepository();
            services.RegistrationContext();
            services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));
        }

        public static void RegistrationControllers(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = false
                });
            });
        }

        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Cinema", new OpenApiInfo { Title = "Кинотетры", Version = "v1" });
                c.SwaggerDoc("Client", new OpenApiInfo { Title = "Клиенты", Version = "v1" });
                c.SwaggerDoc("Film", new OpenApiInfo { Title = "Фильмы", Version = "v1" });
                c.SwaggerDoc("Hall", new OpenApiInfo { Title = "Залы", Version = "v1" });
                c.SwaggerDoc("Staff", new OpenApiInfo { Title = "Персонал", Version = "v1" });
                c.SwaggerDoc("Ticket", new OpenApiInfo { Title = "Билеты", Version = "v1" });
            });
        }

        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Cinema/swagger.json", "Кинотеатры");
                x.SwaggerEndpoint("Client/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Film/swagger.json", "Фильмы");
                x.SwaggerEndpoint("Hall/swagger.json", "Залы");
                x.SwaggerEndpoint("Staff/swagger.json", "Работники");
                x.SwaggerEndpoint("Ticket/swagger.json", "Билеты");
            });
        }
    }
}
