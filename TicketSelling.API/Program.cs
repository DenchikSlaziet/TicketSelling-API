using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TicketSelling.API.AutoMappers;
using TicketSelling.Context;
using TicketSelling.Context.Contracts;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Services.AutoMappers;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

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
    c.SwaggerDoc("Cinema", new OpenApiInfo { Title = "���������", Version = "v1" });
    c.SwaggerDoc("Client", new OpenApiInfo { Title = "�������", Version = "v1" });
    c.SwaggerDoc("Film", new OpenApiInfo { Title = "������", Version = "v1" });
    c.SwaggerDoc("Hall", new OpenApiInfo { Title = "����", Version = "v1" });
    c.SwaggerDoc("Staff", new OpenApiInfo { Title = "��������", Version = "v1" });
    c.SwaggerDoc("Ticket", new OpenApiInfo { Title = "������", Version = "v1" });
});

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketReadRepository, TicketReadRepositiry>();

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IFilmReadRepository, FilmReadRepository>();

builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<ICinemaReadRepository, CinemaReadRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientReadRepository, ClientReadRepository>();

builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IHallReadRepository, HallReadRepository>();

builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffReadRepository, StaffReadRepository>();

builder.Services.AddSingleton<ITicketSellingContext, TicketSellingContext>();

builder.Services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("Cinema/swagger.json", "����������");
        x.SwaggerEndpoint("Client/swagger.json", "�������");
        x.SwaggerEndpoint("Film/swagger.json", "������");
        x.SwaggerEndpoint("Hall/swagger.json", "����");
        x.SwaggerEndpoint("Staff/swagger.json", "���������");
        x.SwaggerEndpoint("Ticket/swagger.json", "������");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
