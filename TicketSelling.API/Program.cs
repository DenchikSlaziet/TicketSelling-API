using TicketSelling.Context;
using TicketSelling.Context.Contracts;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
