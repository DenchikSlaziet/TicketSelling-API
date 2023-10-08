using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TicketSelling.API.AutoMappers;
using TicketSelling.Context.Contracts.Anchors;
using TicketSelling.Repositories.Anchors;
using TicketSelling.ServiceExstansion;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.AutoMappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        CamelCaseText = false
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Cinema", new OpenApiInfo { Title = "Кинотетры", Version = "v1" });
    c.SwaggerDoc("Client", new OpenApiInfo { Title = "Клиенты", Version = "v1" });
    c.SwaggerDoc("Film", new OpenApiInfo { Title = "Фильмы", Version = "v1" });
    c.SwaggerDoc("Hall", new OpenApiInfo { Title = "Залы", Version = "v1" });
    c.SwaggerDoc("Staff", new OpenApiInfo { Title = "Персонал", Version = "v1" });
    c.SwaggerDoc("Ticket", new OpenApiInfo { Title = "Билеты", Version = "v1" });
});

builder.Services.RegistrationOnInterface<IContextAnchor>(ServiceLifetime.Singleton);
builder.Services.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
builder.Services.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("Cinema/swagger.json", "Кинотеатры");
        x.SwaggerEndpoint("Client/swagger.json", "Клиенты");
        x.SwaggerEndpoint("Film/swagger.json", "Фильмы");
        x.SwaggerEndpoint("Hall/swagger.json", "Залы");
        x.SwaggerEndpoint("Staff/swagger.json", "Работники");
        x.SwaggerEndpoint("Ticket/swagger.json", "Билеты");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
